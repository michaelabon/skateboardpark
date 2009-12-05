using System.Drawing;
using System.Windows.Forms;
namespace SkatePark
{
    public partial class Scene
    {

        /// <summary>
        /// Used to know if the user has the mouse down or not.
        /// </summary>
        private bool MouseIsUp { get; set; }
        private Point FirstMouseCoords { get; set; }
        /// <summary>
        /// Determines what to do when user drags mouse.
        /// </summary>
        private DragMode CurrentDragMode { get; set; }
        private MouseButtons PressedMouseButton { get; set; }

        /// <summary>
        /// When the user grags something, this determines where the object originally was
        /// </summary>
        private int FirstDragCoordinate { get; set; }


        public void onMouseWheel(MouseEventArgs e)
        {

            r += (e.Delta > 0 ? -20 : 20);


        }

        public void onMouseDown(MouseEventArgs e)
        {
            // User has mouse on.
            MouseIsUp = false;

            // Save the first coordinates.
            FirstMouseCoords = e.Location;
            PressedMouseButton = e.Button;

            if (e.Button == MouseButtons.Left)
            {
                int blockSelected = IntersectMouse(true);
                // Take into account the grid names
                if (blockSelected >= gameBoard.NumBlocks * gameBoard.NumBlocks)
                    blockSelected -= gameBoard.NumBlocks * gameBoard.NumBlocks;
                if (blockSelected > -1)
                {
                    OnBlockSelected(blockSelected);
                }
            }

        }

        public void onMouseRelease(MouseEventArgs e)
        {
            // User doesn't have mouse down anymore.
            MouseIsUp = true;
            if (e.Button == MouseButtons.Left && CurrentDragMode == DragMode.Rotate)
            {
                SnapBackBlock();
            }
            CurrentDragMode = DragMode.None;
        }

        public void onMouseMove(MouseEventArgs e)
        {
            /**
             * Note: This is a camera movement. NOT an object movement.
             */
            // Did the user have the mouse down?
            if (!MouseIsUp && e.Button == MouseButtons.Middle)
            {
                // We have a drag!
                Point newCoords = e.Location;

                // Compute how much the mouse moved.
                Point dCoords = new Point(FirstMouseCoords.X - e.X, FirstMouseCoords.Y - e.Y);

                // Since this is camera, we're not interested about net movement (ie from mouse down)
                FirstMouseCoords = e.Location;

                UpdateCameraLocationFromDrag(dCoords);
            }
            else if (!MouseIsUp && e.Button == MouseButtons.Left && CurrentDragMode == DragMode.Move)
            {
                // Do an intersect, if it intersects with a different one, move it
                Point saved = FirstMouseCoords;
                FirstMouseCoords = e.Location;
                int otherIntersect = IntersectMouse(false);
                

                if (otherIntersect > -1 && otherIntersect != FirstDragCoordinate)
                {
                    if (MoveBlock(FirstDragCoordinate, otherIntersect))
                        FirstDragCoordinate = otherIntersect;
                    else
                        FirstMouseCoords = saved;
                }
            }
            else if (!MouseIsUp && e.Button == MouseButtons.Left && CurrentDragMode == DragMode.Rotate)
            {
                // Find the difference and call RotateBlock
                AnimateRotateBlock(e.Location.X - FirstMouseCoords.X);
                FirstMouseCoords = e.Location;
            }
        }
    }
}