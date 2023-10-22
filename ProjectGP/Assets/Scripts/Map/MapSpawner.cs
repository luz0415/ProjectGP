using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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

    public List<GameObject> combatMapPrefabs;
    private List<GameObject> originalCombatMapPrefabs = new List<GameObject>();
    public List<GameObject> specialMapPrefabs;

    private GameObject[,] MapUI;
    private GameObject nowMap;

    private void Awake()
    {
        originalCombatMapPrefabs = combatMapPrefabs.ToList();

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
        MapUI = new GameObject[floorPlanX, floorPlanY];
        for (int i = 0; i < floorPlanX; i++)
        {
            for(int j = 0; j < floorPlanY; j++)
            {
                floorPlan[i, j] = null;
                MapUI[i, j] = UiManager.instance.MapUI.transform.GetChild(i).GetChild(j).gameObject;
                MapUI[i, j].SetActive(false);
                for(int k = 0; k < MapUI[i, j].transform.childCount; k++)
                {
                    MapUI[i, j].transform.GetChild(k).gameObject.SetActive(false);
                }
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


        for (int i = 0; i < floorPlanX; i++)
        {
            for (int j = 0; j < floorPlanY; j++)
            {
                if (floorPlan[i, j] != null)
                {
                    MapUI[i, j].transform.parent.gameObject.SetActive(true);
                    MapUI[i, j].SetActive(true);
                    for(int direction = 0; direction < 4; direction++)
                    {
                        if (floorPlan[i, j].neighborMap[direction] != null)
                        {
                            int reverseDirection;
                            if (direction >= 2)
                            {
                                reverseDirection = direction - 2;
                            }
                            else
                            {
                                reverseDirection = direction + 2;
                            }

                            MapUI[i, j].transform.GetChild(reverseDirection).gameObject.SetActive(true);
                        }
                    }
                }
            }
        }

        int fullRoomChildIndex = MapUI[baseRoomCoord.x, baseRoomCoord.y].transform.childCount - 1;
        nowMap = MapUI[baseRoomCoord.x, baseRoomCoord.y];
        nowMap.transform.GetChild(fullRoomChildIndex).gameObject.SetActive(true);
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
        newRoom.map.roomCoord[0] = newRoom.coord.x; newRoom.map.roomCoord[1] = newRoom.coord.y;

        roomCount++;
    }

    GameObject SelectRoomPrefab()
    {
        int leftRoomCount = maxRoomCount - roomCount;
        GameObject selectedRoom;
        int roomIndex;

        if (leftRoomCount == specialMapPrefabs.Count)
        {
            roomIndex = Random.Range(0, specialMapPrefabs.Count);
            selectedRoom = specialMapPrefabs[roomIndex];
            specialMapPrefabs.RemoveAt(roomIndex);
        }
        else
        {
            roomIndex = Random.Range(0, combatMapPrefabs.Count);
            selectedRoom = combatMapPrefabs[roomIndex];
            combatMapPrefabs.RemoveAt(roomIndex);

            if (combatMapPrefabs.Count == 0)
            {
                combatMapPrefabs = originalCombatMapPrefabs.ToList();
            }
        }
        return selectedRoom;
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

    public void RoomUIChange(int nowX, int nowY, int direction)
    {
        int fullRoomChildIndex = nowMap.transform.childCount - 1;
        nowMap.transform.GetChild(fullRoomChildIndex).gameObject.SetActive(false);

        // »óÇÏÁÂ¿ì
        int[] dx = { 0, 0, -1, 1 };
        int[] dy = { -1, 1, 0, 0 };
        nowMap = MapUI[nowX + dx[direction], nowY + dy[direction]];
        nowMap.transform.GetChild(fullRoomChildIndex).gameObject.SetActive(true);

    }
}
