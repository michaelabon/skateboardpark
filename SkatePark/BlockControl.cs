using System;
namespace SkatePark
{
    public enum ToolPanelCommand
    {
        None,
        CameraDrag,
        BlockDrag, BlockAdd, BlockDelete
    }

    public enum DragMode
    {
        None, Move, Rotate, Zoom
    }

    public partial class Scene
    {
        public ToolPanelCommand SelectedCommand { get; set; }
        public DragMode SelectedDragMode { get; set; }
        public string SelectedBlockAdd { get; set; }

        private ICubelet[] gridArray;

        private void InitializeGridArray()
        {
            gridArray = new ICubelet[gameBoard.NumBlocks * gameBoard.NumBlocks];
        }

        private void OnBlockSelected(int blockNum)
        {
            
            switch (SelectedCommand)
            {
                case ToolPanelCommand.CameraDrag:
                    // Do nothing
                    break;
                case ToolPanelCommand.BlockDelete:
                    DeleteBlock(blockNum);
                    break;
                case ToolPanelCommand.BlockDrag:
                    if (IsBlockExists(blockNum) != null)
                    {
                        // First, tell everything that the user clicked here.
                        FirstDragCoordinate = blockNum;

                        if (SelectedDragMode == DragMode.Move)
                        {
                            CurrentDragMode = DragMode.Move;
                        }
                        else if (SelectedDragMode == DragMode.Rotate)
                        {
                            CurrentDragMode = DragMode.Rotate;
                        }
                    }
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

        private void MoveBlock(int firstCoordinate, int newCoordinate)
        {
            // Make sure the new block isn't full
            if (IsBlockExists(newCoordinate) != null)
            {
                return;
            }

            // Move it!
            ICubelet block = gridArray[firstCoordinate];
            block.PosX = newCoordinate % gameBoard.NumBlocks;
            block.PosY = newCoordinate / gameBoard.NumBlocks;

            // Delete old location
            gridArray[firstCoordinate] = null;
            // Move to new location
            gridArray[newCoordinate] = block;
        }

        private void AnimateRotateBlock(int dX)
        {
            // Rotate it!
            ICubelet block = gridArray[FirstDragCoordinate];
            block.Orientation += dX;
        }

        private void SnapBackBlock()
        {
            ICubelet block = gridArray[FirstDragCoordinate];

            
            block.Orientation %= 360;
            if (block.Orientation < 0)
            {
                block.Orientation += 360;
            }

            
            for( int angle = 0; angle < 360; angle += 90)
            {
                if( block.Orientation >= angle && block.Orientation < angle + 90 )
                {
                    // Find out which is closer.
                    int first = Math.Abs(angle - block.Orientation);
                    int second = Math.Abs(angle + 90 - block.Orientation);

                    if (first <= second)
                    {
                        block.Orientation = angle;
                    }
                    else
                    {
                        block.Orientation = angle + 90;
                    }
                }
            }
        }
    }
}