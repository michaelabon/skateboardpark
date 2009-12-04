using System;
namespace SkatePark
{
    public enum ToolPanelCommand
    {
        CameraPan, CameraMove, CameraRotate, CameraZoom,
        BlockDelete, BlockRotate, BlockAdd
    }

    public partial class Scene
    {
        private ToolPanelCommand SelectedCommand { get; set; }
        private string SelectedBlockAdd { get; set; }

        private ICubelet[] gridArray;

        private void InitializeGridArray()
        {
            gridArray = new ICubelet[gameBoard.NumBlocks * gameBoard.NumBlocks];
        }

        private void OnBlockSelected(int blockNum)
        {
            switch (SelectedCommand)
            {
                case ToolPanelCommand.CameraPan:
                case ToolPanelCommand.CameraMove:
                case ToolPanelCommand.CameraRotate:
                case ToolPanelCommand.CameraZoom:
                    // Do nothing
                    break;
                case ToolPanelCommand.BlockDelete:
                    DeleteBlock(blockNum);
                    break;
                case ToolPanelCommand.BlockRotate:
                    break;
                case ToolPanelCommand.BlockAdd:
                    BlockAdd(blockNum);
                    break;
                default:
                    break; // Do nothing
            }
        }

        private ICubelet IsBlockExists(int blockNum)
        {
            return gridArray[blockNum];
        }

        private void BlockAdd(int blockNum)
        {
            if (IsBlockExists(blockNum) != null)
            {
                // Do nothing
                return;
            }

            ICubelet newCube = new ICubelet(SelectedBlockAdd);
            newCube.PosX = blockNum % gameBoard.NumBlocks;
            newCube.PosY = blockNum / gameBoard.NumBlocks;

            drawables.Add(newCube);
            gridArray[blockNum] = newCube;
        }

        private void DeleteBlock(int blockNum)
        {
            ICubelet block = IsBlockExists(blockNum);

            if (block != null)
            {
                gridArray[blockNum] = null;
                drawables.Remove(block);
            }
        }
    }
}