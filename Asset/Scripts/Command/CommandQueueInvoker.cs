using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// CommandList class to handle a queue of commands and execution status
public class CommandDelayList
{
    // public List<ICommandDelay> s_ExecutedCommands = new List<ICommandDelay>();
    // public int currentCommandIndex = -1;
    public Queue<ICommandDelay> s_CommandQueue = new Queue<ICommandDelay>();
    public bool isExecuting = false;
}

// CommandQueueInvoker class
public class CommandQueueInvoker : Singleton<CommandQueueInvoker>
{
    // Command dictionary to manage command queues by ObjectId and IdName
    private Dictionary<string, Dictionary<string, CommandDelayList>> m_CommandObjectDict = new();

    void Update()
    {
        if (m_CommandObjectDict.Count == 0) return;

        foreach (var m_CommandDict in m_CommandObjectDict.Values)
        {
            if (m_CommandDict == null || m_CommandDict.Count == 0) continue;

            // Iterate over the dictionary and process commands if not already executing
            foreach (var item in m_CommandDict)
            {
                CommandDelayList commandList = item.Value;
                if (commandList == null) continue;

                if (!commandList.isExecuting && commandList.s_CommandQueue.Count > 0)
                {
                    StartCoroutine(ProcessCommands(commandList));
                }
            }
        }
    }

    // Method to execute a command
    public void ExecuteCommand(ICommandDelay command, int maxSize)
    {
        //4
        if (command == null)
        {
            Debug.LogWarning("Command is null. Skipping execution.");
            return;
        }
        string ObjectIdString = command.ObjectId.ToString();
        if (!m_CommandObjectDict.ContainsKey(ObjectIdString))
        {
            m_CommandObjectDict[ObjectIdString] = new Dictionary<string, CommandDelayList>();
        }

        if (!m_CommandObjectDict[ObjectIdString].ContainsKey(command.IdName))
        {
            m_CommandObjectDict[ObjectIdString][command.IdName] = new CommandDelayList();
        }
        //Debug.Log("(4)ExecuteCommand " + GetTime.GetCurrentTime("full-ms"));

        CommandDelayList commandList = m_CommandObjectDict[ObjectIdString][command.IdName];
        commandList.s_CommandQueue.Enqueue(command);

        Debug.Log($"Command added to queue: {command.IdName}");

        // Trim undo/redo list
        // if (commandList.currentCommandIndex < commandList.s_ExecutedCommands.Count - 1)
        // {
        //     commandList.s_ExecutedCommands.RemoveRange(
        //         commandList.currentCommandIndex + 1,
        //         commandList.s_ExecutedCommands.Count - (commandList.currentCommandIndex + 1)
        //     );
        // }

        // commandList.s_ExecutedCommands.Add(command);

        // Enforce max size of command queue
        if (commandList.s_CommandQueue.Count > maxSize)
        {
            commandList.s_CommandQueue.Dequeue();
        }
    }

    // Process commands for a specific CommandList
    private IEnumerator ProcessCommands(CommandDelayList commandList)
    {
        //5
        if (commandList == null)
        {
            Debug.LogWarning("CommandList is null. Skipping processing.");
            yield break;
        }

        commandList.isExecuting = true;

        while (commandList.s_CommandQueue.Count > 0)
        {
            ICommandDelay currentCommand = commandList.s_CommandQueue.First();
            //ICommandDelay currentCommand = commandList.s_CommandQueue.Dequeue();

            if (currentCommand == null)
            {
                Debug.LogWarning("Command in queue is null. Skipping.");
                continue;
            }

            //Debug.Log("(5)ProcessCommands " + GetTime.GetCurrentTime("full-ms"));
            //commandList.currentCommandIndex++;
            StartCoroutine(currentCommand.ExecuteDelay());

            yield return new WaitForSeconds(currentCommand.Cooldown);
            commandList.s_CommandQueue.Dequeue();
        }

        commandList.isExecuting = false;
    }

    // // Undo the last command
    // public void UndoCommand(string objectIdString, string idName)
    // {
    //     if (!m_CommandObjectDict.ContainsKey(objectIdString) || !m_CommandObjectDict[objectIdString].ContainsKey(idName))
    //     {
    //         Debug.LogWarning($"No commands found for ObjectId: {objectIdString}, IdName: {idName}. Cannot undo.");
    //         return;
    //     }

    //     CommandList commandList = m_CommandObjectDict[objectIdString][idName];
    //     if (commandList == null || commandList.s_CommandQueue == null)
    //     {
    //         Debug.LogWarning($"CommandList for ObjectId: {objectIdString}, IdName: {idName} is null. Cannot undo.");
    //         return;
    //     }

    //     if (commandList.s_CommandQueue.Count > 0)
    //     {
    //         Debug.LogWarning("Commands in the queue cannot be undone. Only executed commands can be undone.");
    //         return;
    //     }

    //     if (commandList.s_ExecutedCommands.Count == 0 || commandList.currentCommandIndex < 0)
    //     {
    //         Debug.LogWarning("No commands to undo.");
    //         return;
    //     }

    //     ICommandDelay commandToUndo = commandList.s_ExecutedCommands[commandList.currentCommandIndex];
    //     commandToUndo.Undo();
    //     commandList.currentCommandIndex--;
    //     Debug.Log("Undo executed successfully.");
    // }

    // // Redo the previously undone command
    // public void RedoCommand(string objectIdString, string idName)
    // {
    //     if (!m_CommandObjectDict.ContainsKey(objectIdString) || !m_CommandObjectDict[objectIdString].ContainsKey(idName))
    //     {
    //         Debug.LogWarning($"No commands found for ObjectId: {objectIdString}, IdName: {idName}. Cannot redo.");
    //         return;
    //     }

    //     CommandList commandList = m_CommandObjectDict[objectIdString][idName];
    //     if (commandList == null || commandList.s_CommandQueue == null)
    //     {
    //         Debug.LogWarning($"CommandList for ObjectId: {objectIdString}, IdName: {idName} is null. Cannot redo.");
    //         return;
    //     }

    //     if (commandList.currentCommandIndex >= commandList.s_ExecutedCommands.Count - 1)
    //     {
    //         Debug.LogWarning("No commands to redo.");
    //         return;
    //     }

    //     commandList.currentCommandIndex++;
    //     ICommandDelay commandToRedo = commandList.s_ExecutedCommands[commandList.currentCommandIndex];
    //     commandToRedo.Execute();
    //     Debug.Log("Redo executed successfully.");
    // }
}
