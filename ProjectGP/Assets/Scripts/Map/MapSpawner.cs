using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    public class RoomCoord
    {
        public int x;
        public int y;
        public RoomCoord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public RoomCoord()
        {
            x = 0;
            y = 0;
        }
    }

    public class Room
    {
        public RoomCoord coord;
        public Map map;

        public Room(int x, int y, Map map)
        {
            coord.x = x;
            coord.y = y;
            this.map = map;
        }
        public Room(RoomCoord coord, Map map)
        {
            this.coord = coord;
            this.map = map;
        }
    }

    public int nowFloor;
    public int maxRoomRandomNumber = 2;
    private int maxRoomCount;
    private int roomCount;

    public Vector3 baseRoomPos = Vector3.zero;
    public float distanceBetRoom = 100f;

    public int floorPlanX = 9;
    public int floorPlanY = 8;

    private Map[,] floorPlan;

    private Queue<Room> roomSpawnQueue = new Queue<Room>();

    public GameObject[] combatMapPrefabs;

    private void Awake()
    {
        SetMaxRoomCount();
        FloorPlanInitialization();
        MakeFloorPlan();
    }

    private void SetMaxRoomCount()
    {
        maxRoomCount = Random.Range(0, maxRoomRandomNumber) + 5 + (int)(nowFloor * 2.5);
    }

    private void FloorPlanInitialization()
    {
        floorPlan = new Map[floorPlanX, floorPlanY];
        for(int i = 0; i < floorPlanX; i++)
        {
            for(int j = 0; j < floorPlanY; j++)
            {
                floorPlan[i, j] = null;
            }
        }

    }

    private void MakeFloorPlan()
    {
        RoomCoord baseRoomCoord = new RoomCoord(floorPlanX / 2, floorPlanY / 2);
        Room baseRoom = new Room(baseRoomCoord, null);
        RoomInstantiate(baseRoom, null);
        floorPlan[baseRoom.coord.x, baseRoom.coord.y] = baseRoom.map;

        roomSpawnQueue.Enqueue(baseRoom);

        // »óÇÏÁÂ¿ì
        int[] dx = { 0, 0, -1, 1 };
        int[] dy = { -1, 1, 0, 0 };

        while (roomSpawnQueue.Count > 0)
        {
            Room nowRoom = roomSpawnQueue.Dequeue();


            for (int direction = 0; direction < 4; direction++)
            {
                int nx = nowRoom.coord.x + dx[direction];
                int ny = nowRoom.coord.y + dy[direction];

                if (nx < 0 || ny < 0 || nx >= floorPlanX || ny >= floorPlanY) continue;

                if (roomCount >= maxRoomCount || Random.value < 0.5)
                {
                    continue;
                }

                RoomCoord neighborRoomCoord = new RoomCoord(nx, ny);
                Room neighborRoom = new Room(neighborRoomCoord, null);

                if (floorPlan[neighborRoom.coord.x, neighborRoom.coord.y] == null)
                {
                    RoomInstantiate(neighborRoom, nowRoom, direction);
                    floorPlan[neighborRoom.coord.x, neighborRoom.coord.y] = neighborRoom.map;
                }
                else
                {
                    neighborRoom.map = floorPlan[neighborRoom.coord.x, neighborRoom.coord.y];
                }

                if (IsRoomConnected(nowRoom, direction))
                {
                    continue;
                }

                if (IsNeighborCountOver(neighborRoom, 2))
                {
                    continue;
                }

                MakeConnection(nowRoom, neighborRoom, direction);
                roomSpawnQueue.Enqueue(neighborRoom);
            }

            if(roomSpawnQueue.Count == 0 && roomCount < maxRoomCount)
            {
                roomSpawnQueue.Enqueue(nowRoom);
            }
        }

    }

    void RoomInstantiate(Room newRoom, Room basisRoom, int direction = -1)
    {
        Vector3 newRoomPos;
        if (basisRoom == null)
        {
            newRoomPos = baseRoomPos;

        }
        else
        {
            Vector3 basisPos = basisRoom.map.transform.position;

            switch (direction)
            {
                case 0:
                    newRoomPos = new Vector3(basisPos.x, basisPos.y, basisPos.z + distanceBetRoom);
                    break;
                case 1:
                    newRoomPos = new Vector3(basisPos.x, basisPos.y, basisPos.z - distanceBetRoom);
                    break;
                case 2:
                    newRoomPos = new Vector3(basisPos.x - distanceBetRoom, basisPos.y, basisPos.z);
                    break;
                default:
                    newRoomPos = new Vector3(basisPos.x + distanceBetRoom, basisPos.y, basisPos.z);
                    break;
            }
        }

        GameObject roomPrefab = SelectRoomPrefab();
        newRoom.map = Instantiate(roomPrefab, newRoomPos, Quaternion.identity).GetComponent<Map>();

        roomCount++;
    }

    GameObject SelectRoomPrefab()
    {
        int roomIndex = Random.Range(0, combatMapPrefabs.Length);
        return combatMapPrefabs[roomIndex];
    }


    bool IsRoomConnected(Room room, int direction)
    {
        if (room.map.neighborMap[direction] != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool IsNeighborCountOver(Room room, int overNumber)
    {
        if(room.map == null)
        {
            print(room.coord.x + ", " + room.coord.y);
        }
        if(room.map.neighborCount >= overNumber)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void MakeConnection(Room standardRoom, Room neighborRoom ,int direction)
    {
        int reverseDirection;
        if (direction % 2 == 0)
        {
            reverseDirection = direction + 1;
        }
        else
        {
            reverseDirection = direction - 1;
        }

        neighborRoom.map.neighborMap[reverseDirection] = standardRoom.map;
        standardRoom.map.neighborMap[direction] = neighborRoom.map;
    }
}
