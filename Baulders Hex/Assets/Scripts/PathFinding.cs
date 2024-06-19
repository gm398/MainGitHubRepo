using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathFinding
{
    //draw a diagram
    //the A* search algorithm
    public bool AStarSearch(HexGrid hexGrid, 
        HexTile start, 
        HexTile goal, 
        out List<HexTile> 
            rout, 
        float heightStep, 
        GameObject unit, 
        out Dictionary<HexTile, HexTile> visited, 
        int range)
    {
        visited = null;
        Dictionary<HexTile, HexTile> visitedHexs = new Dictionary<HexTile, HexTile>();
        Dictionary<HexTile, HexTile> cameFrom = new Dictionary<HexTile, HexTile>();
        Dictionary<HexTile, float> costSoFar = new Dictionary<HexTile, float>();
        HexQueue hexQueue = new HexQueue();
        bool canFly = false;
        
        List<HexTile> startNeighbours = hexGrid.GetNeighbours(start);
        bool surrounded = true;
        foreach(HexTile h in startNeighbours)
        {
            if (!h.IsOccupied)
            {
                if(canFly || h.IsTraversable)
                {
                    surrounded = false;
                }
            }
        }
        if (surrounded) { rout = null; return false; }
        visitedHexs.Add(start, start);
        cameFrom.Add(start, start);
        costSoFar.Add(start, 0);
        hexQueue.Add(start, 0);
        int loopCount = 0;
        bool doable = canFly;
        if (!canFly)
        {
            doable = ChooseBetterGoal(hexGrid, start, out goal, goal, unit, 5);
        }
        while (!hexQueue.IsEmpty() && doable)
        {
            loopCount++;
            PriorityHex currentPrioHex = hexQueue.GetNext();
            HexTile currentHex = currentPrioHex.hex;
            
            if (currentHex == goal || Hexagon.DistanceBetweenHexs(goal.Hexagon, currentHex.Hexagon) <= range) {
                // Debug.Log("reached goal");
                goal = currentHex;
                break; }

            foreach (HexTile nextHex in hexGrid.GetNeighbours(currentHex))
            {
                float heightDiff = Mathf.Abs(nextHex.Height - currentHex.Height);
                if ((nextHex.IsTraversable || canFly) 
                    && heightDiff <= heightStep 
                    && ((//unit == null 
                        nextHex.IsOccupied 
                        //|| nextHex.GetOccupant().Equals(unit)
                       //|| (enemyLayers == (enemyLayers & (1 << nextHex.GetOccupant().layer)))
                        )
                    )) 
                {
                    float newCost = costSoFar[currentHex];
                    if (canFly) { newCost += 1; }
                    else { newCost += nextHex.MoveDifficulty; }

                    float costOfNext = 0;
                    if (costSoFar.TryGetValue(nextHex, out var value)) { costOfNext = value; }
                    if (!visitedHexs.ContainsKey(nextHex) || newCost < costOfNext)
                    {
                        
                        costSoFar[nextHex] = newCost;
                        float priority = newCost + Hexagon.DistanceBetweenHexs(nextHex.Hexagon, goal.Hexagon);
                        hexQueue.Add(nextHex, priority);

                        cameFrom[nextHex] = currentHex;
                        visitedHexs.TryAdd(nextHex, nextHex);
                        
                    }
                }
            }
            
        }
        //Debug.Log("a* looped: " + loopCount + " times");
        visited = visitedHexs;
        //if the goal is unreachable the rout will be null and false will be returned 
        if (!visitedHexs.ContainsKey(goal)) { Debug.Log("No path available"); rout = null; return false; }
        rout = ReconstructPath(start, goal, cameFrom);
        return true;
    }

    private List<HexTile> ReconstructPath(HexTile start, HexTile goal, Dictionary<HexTile, HexTile> cameFrom)
    {
        List<HexTile> path = new List<HexTile>();
        HexTile current = goal;
        while(current != start)
        {
            path.Add(current);
            current = cameFrom[current];
        }
        path.Reverse();
        return path;
    }
    


    // if the goal is not traversable try to find a traversable hex in its neighbours to reduce hexs checked
    private bool ChooseBetterGoal(HexGrid hexGrid, HexTile start, out HexTile newGoal, HexTile goal, GameObject unit, int attempts)
    {
        if (start == goal) { newGoal = goal; return true; }

        float distance = Hexagon.DistanceBetweenHexs(start.Hexagon, goal.Hexagon);
        bool newHexFound = false;
        bool sucess = false;
        

        List<HexTile> neighbours = hexGrid.GetNeighbours(goal);
        if (!goal.IsTraversable)
        {
            float dis;
            foreach (HexTile h in neighbours)
            {
                dis = Hexagon.DistanceBetweenHexs(start.Hexagon, h.Hexagon);
                if (!newHexFound) { if (h.IsTraversable /* && !unit.Equals(h.GetOccupant())*/) { goal = h; newHexFound = true; } }
                else if (h.IsTraversable && dis < distance /*&& !unit.Equals(h.GetOccupant())*/)
                {
                    distance = dis;
                    goal = h;
                }
            }
            sucess = newHexFound;
            if (!newHexFound && attempts > 0)
            {
                foreach (HexTile h in neighbours)
                {
                    dis = Hexagon.DistanceBetweenHexs(start.Hexagon, h.Hexagon);
                    if (dis < distance) { goal = h; newHexFound = true; }
                }
                if (newHexFound)
                {
                    sucess = ChooseBetterGoal(hexGrid, start, out goal, goal, unit, attempts - 1);
                }
            }
        }
        else { sucess = true; }
        newGoal = goal;
        return sucess;
    }

    private class PriorityHex
    {
        public HexTile hex;
        public float priority;

        public PriorityHex(HexTile hex, float priority)
        {
            this.hex = hex;
            this.priority = priority;
        }
        public int CompareTo(PriorityHex compareHex)
        {
            if (compareHex == null) { return 1; }
            else { return this.priority.CompareTo(compareHex.priority); }
        }
        public float GetPriority() { return priority; }
    }

    private class HexQueue
    {
        private List<PriorityHex> hexQueue = new List<PriorityHex>();

        public void Add(HexTile hex, float priority)
        {
            PriorityHex pHex = new PriorityHex(hex, priority);
            hexQueue.Add(pHex);
            //Debug.Log("hex added to the hexQueue");
        }
      
        void SortList()
        {
            hexQueue.Sort();
        }

        public PriorityHex GetNext()
        {

            float currentPrio = -1;
            PriorityHex hexReturn = null;
            foreach (PriorityHex ph in hexQueue)
            {
                if(currentPrio < 0)
                {
                    hexReturn = ph;
                    currentPrio = ph.priority;
                }
                else
                {
                    if(currentPrio >= ph.priority)
                    {
                        hexReturn = ph;
                        currentPrio = ph.priority;
                    }
                }
            }

            hexQueue.Remove(hexReturn);
            return hexReturn;
        }

        public bool IsEmpty()
        {
            return hexQueue.Count <= 0;
        }

    }
}
