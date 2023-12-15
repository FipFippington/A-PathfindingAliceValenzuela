using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class PathMaker
{
    public MapLocation location;
    public float G;
    public float H;
    public float F;
    public GameObject marker;
    public PathMaker parent;

    public PathMaker(MapLocation l, float g, float h, float f, GameObject marker, PathMaker p)
    {
        location = l;
        G = g;
        H = h;
        F = f;
        this.marker = marker;
        parent = p;
    }
    public override bool Equals(object obj)
    {
        if ((obj == null) || !this.GetType().Equals(obj.GetType()))
        {
            return false;
        }
        else
        {
            return location.Equals(((PathMaker)obj).location);
        }
    }
    public override int GetHashCode()
    {
        return 0;
    }
}

public class FindPathAStar : MonoBehaviour
{
    public Maze maze;
    public Material closedMaterial;
    public Material openMaterial;

    List<PathMaker> open = new List<PathMaker>();
    List<PathMaker> closed = new List<PathMaker>();

    public GameObject start;
    public GameObject end;
    public GameObject pathP;

    PathMaker goalNode;
    PathMaker startNode;

    PathMaker lastPos;
    bool done = false;

    void removeAllMarkers()
    {
        GameObject[] markers = GameObject.FindGameObjectsWithTag("marker");
        foreach (GameObject m in markers)
            Destroy(m);
    }

    void beginSearch()
    {
        done = false;
        removeAllMarkers();

        List<MapLocation> locations = new List<MapLocation>();
        for (int z = 1; z < maze.depth - 1; z++)
            for (int x = 1; x < maze.width - 1; x++)
            {
                if (maze.map[x, z] != 1)
                    locations.Add(new MapLocation(x, z));
            }

        locations.Shuffle();

        Vector3 startLoacation = new Vector3(locations[0].x * maze.scale, 0, locations[0].z * maze.scale);
        startNode = new PathMaker(new MapLocation(locations[0].x, locations[0].z), 0, 0, 0,
            Instantiate(start, startLoacation, Quaternion.identity), null);

        Vector3 goalLoacation = new Vector3(locations[1].x * maze.scale, 0, locations[1].z * maze.scale);
        startNode = new PathMaker(new MapLocation(locations[0].x, locations[1].z), 0, 0, 0,
            Instantiate(end, goalLoacation, Quaternion.identity), null);

        open.Clear();
        closed.Clear();
        open.Add(startNode);
        lastPos = startNode;
    }

    void Search(PathMaker thisNode)
    {
        if (thisNode.Equals(goalNode)) { done = true;  return; }

        foreach (MapLocation dir in maze.directions)
        {

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) beginSearch();
    }
}
