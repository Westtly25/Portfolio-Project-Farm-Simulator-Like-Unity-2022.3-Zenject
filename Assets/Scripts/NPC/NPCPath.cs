using System;
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(NPCMovement))]
public class NPCPath : MonoBehaviour
{
    public Stack<NPCMovementStep> npcMovementStepStack;

    private NPCMovement npcMovement;

    private void Awake()
    {
        npcMovement = GetComponent<NPCMovement>();
        npcMovementStepStack = new Stack<NPCMovementStep>();
    }

    public void ClearPath() =>
        npcMovementStepStack.Clear();

    public void BuildPath(NPCScheduleEvent npcScheduleEvent)
    {
        ClearPath();

        if (npcScheduleEvent.toSceneName == npcMovement.npcCurrentScene)
        {
            Vector2Int npcCurrentGridPosition = (Vector2Int)npcMovement.npcCurrentGridPosition;

            Vector2Int npcTargetGridPosition = (Vector2Int)npcScheduleEvent.toGridCoordinate;

            NPCManager.Instance.BuildPath(npcScheduleEvent.toSceneName, npcCurrentGridPosition, npcTargetGridPosition, npcMovementStepStack);
        }
        else if (npcScheduleEvent.toSceneName != npcMovement.npcCurrentScene)
        {
            SceneRoute sceneRoute;

            sceneRoute = NPCManager.Instance.GetSceneRoute(npcMovement.npcCurrentScene.ToString(), npcScheduleEvent.toSceneName.ToString());

            if (sceneRoute != null)
            {
                for (int i = sceneRoute.scenePathList.Count - 1; i >= 0; i--)
                {
                    int toGridX, toGridY, fromGridX, fromGridY;

                    ScenePath scenePath = sceneRoute.scenePathList[i];

                    if (scenePath.toGridCell.X >= StaticData.maxGridWidth || scenePath.toGridCell.Y >= StaticData.maxGridHeight)
                    {
                        toGridX = npcScheduleEvent.toGridCoordinate.X;
                        toGridY = npcScheduleEvent.toGridCoordinate.Y;
                    }
                    else
                    {
                        toGridX = scenePath.toGridCell.X;
                        toGridY = scenePath.toGridCell.Y;
                    }

                    if (scenePath.fromGridCell.X >= StaticData.maxGridWidth || scenePath.fromGridCell.Y >= StaticData.maxGridHeight)
                    {
                        fromGridX = npcMovement.npcCurrentGridPosition.x;
                        fromGridY = npcMovement.npcCurrentGridPosition.y;
                    }
                    else
                    {
                        fromGridX = scenePath.fromGridCell.X;
                        fromGridY = scenePath.fromGridCell.Y;
                    }

                    Vector2Int fromGridPosition = new Vector2Int(fromGridX, fromGridY);

                    Vector2Int toGridPosition = new Vector2Int(toGridX, toGridY);

                    NPCManager.Instance.BuildPath(scenePath.sceneName, fromGridPosition, toGridPosition, npcMovementStepStack);
                }
            }
        }


        if (npcMovementStepStack.Count > 1)
        {
            UpdateTimesOnPath();
            npcMovementStepStack.Pop();

            npcMovement.SetScheduleEventDetails(npcScheduleEvent);
        }

    }

    public void UpdateTimesOnPath()
    {
        TimeSpan currentGameTime = TimeManager.Instance.GetGameTime();

        NPCMovementStep previousNPCMovementStep = null;

        foreach (NPCMovementStep npcMovementStep in npcMovementStepStack)
        {
            if (previousNPCMovementStep == null)
                previousNPCMovementStep = npcMovementStep;

            npcMovementStep.Hour = currentGameTime.Hours;
            npcMovementStep.Minute = currentGameTime.Minutes;
            npcMovementStep.Second = currentGameTime.Seconds;

            TimeSpan movementTimeStep;

            if (MovementIsDiagonal(npcMovementStep, previousNPCMovementStep))
            {
                movementTimeStep = new TimeSpan(0, 0, (int)(StaticData.gridCellDiagonalSize / StaticData.secondsPerGameSecond / npcMovement.npcNormalSpeed));
            }
            else
            {
                movementTimeStep = new TimeSpan(0, 0, (int)(StaticData.gridCellSize / StaticData.secondsPerGameSecond / npcMovement.npcNormalSpeed));
            }

            currentGameTime = currentGameTime.Add(movementTimeStep);

            previousNPCMovementStep = npcMovementStep;
        }

    }
  
    private bool MovementIsDiagonal(NPCMovementStep npcMovementStep, NPCMovementStep previousNPCMovementStep)
    {
        if ((npcMovementStep.GridCoordinate.x != previousNPCMovementStep.GridCoordinate.x) &&
            (npcMovementStep.GridCoordinate.y != previousNPCMovementStep.GridCoordinate.y))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}