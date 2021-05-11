using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoomPlacer : MonoBehaviour
{
    public Room[] RoomPrefabs;  // доступные комнаты
    public Room StartingRoom;   // стартовая комната
    private Room[,] spawnedRooms;   // уже созданные комнаты

    private IEnumerator Start()
    {
        spawnedRooms = new Room[11, 11];    // будет поле 11 на 11
        spawnedRooms[5, 5] = StartingRoom;  // в центр ставим стартовую комнату

        // вызываем построение комнат 12 раз (случ. число)
        for(int i = 0; i < 12; i++)
        {
            PlaceOneRoom();
            yield return new WaitForSecondsRealtime(0.5f);  // для пошаговой генерации с задержкой в пол секунды
        }
    }

    private void PlaceOneRoom()
    {
        // списоек мест, где можем заспавнить комнату (места рядом с сущесвующими комнатами, чтобы был переход)
        HashSet<Vector2Int> vacantPlaces = new HashSet<Vector2Int>();

        for (int x = 0; x < spawnedRooms.GetLength(0); x++)
        {
            for (int y = 0; y < spawnedRooms.GetLength(1); y++)
            {
                if (spawnedRooms[x, y] == null) continue;

                int maxX = spawnedRooms.GetLength(0) - 1;
                int maxY = spawnedRooms.GetLength(1) - 1;
                if (x > 0 && spawnedRooms[x - 1, y] == null) vacantPlaces.Add(new Vector2Int(x - 1, y));    // комнаты слева
                if (y > 0 && spawnedRooms[x, y - 1] == null) vacantPlaces.Add(new Vector2Int(x, y - 1));    // комнаты снизу
                if (x < maxX && spawnedRooms[x + 1, y] == null) vacantPlaces.Add(new Vector2Int(x + 1, y));    // комнаты справа
                if (y < maxY && spawnedRooms[x, y + 1] == null) vacantPlaces.Add(new Vector2Int(x, y + 1));    // комнаты сверху
            }
        }

        Room newRoom = Instantiate(RoomPrefabs[Random.Range(0, RoomPrefabs.Length)]);

        int limit = 500;
        while (limit -- > 0)
        {
            Vector2Int position = vacantPlaces.ElementAt(Random.Range(0, vacantPlaces.Count));
            // newRoom.RotateRandomly();

            if (ConnectToSomething(newRoom, position))
            {
                newRoom.transform.position = new Vector3(position.x - 5, 0, position.y - 5) * 12;   // 12 x 12 - размер комнат
                spawnedRooms[position.x, position.y] = newRoom;
                return;
            }
        }

        Destroy(newRoom.gameObject); // если дошли сюда, то комната не нужна т.к. в нее не будет доступа
        Debug.Log("Выброс");
    }

    private bool ConnectToSomething(Room room, Vector2Int pos)
    {
        int maxX = spawnedRooms.GetLength(0) - 1;
        int maxY = spawnedRooms.GetLength(1) - 1;

        // список вариантов к чему подсоедениться
        List<Vector2Int> neighbours = new List<Vector2Int>();

        if (room.DoorU != null && pos.y < maxY && spawnedRooms[pos.x, pos.y + 1]?.DoorD != null) neighbours.Add(Vector2Int.up);
        if (room.DoorD != null && pos.y > 0 && spawnedRooms[pos.x, pos.y - 1]?.DoorU != null) neighbours.Add(Vector2Int.down);
        if (room.DoorR != null && pos.x < maxX && spawnedRooms[pos.x  + 1, pos.y]?.DoorL != null) neighbours.Add(Vector2Int.right);
        if (room.DoorL != null && pos.x > 0 && spawnedRooms[pos.x - 1, pos.y]?.DoorR != null) neighbours.Add(Vector2Int.left);

        if (neighbours.Count == 0) return false;

        Vector2Int selectedDirection = neighbours[Random.Range(0, neighbours.Count)];
        Room selectedRoom = spawnedRooms[pos.x + selectedDirection.x, pos.y + selectedDirection.y];

        if (selectedDirection == Vector2Int.up)
        {
            room.DoorU.SetActive(false);
            selectedRoom.DoorD.SetActive(false);
        }
        else if (selectedDirection == Vector2Int.down)
        {
            room.DoorD.SetActive(false);
            selectedRoom.DoorU.SetActive(false);
        }
        else if (selectedDirection == Vector2Int.right)
        {
            room.DoorR.SetActive(false);
            selectedRoom.DoorL.SetActive(false);
        }
        else if (selectedDirection == Vector2Int.left)
        {
            room.DoorL.SetActive(false);
            selectedRoom.DoorR.SetActive(false);
        }
        return true;
    }
    
}
