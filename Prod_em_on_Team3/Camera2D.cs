using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System.Collections.Generic;
using System.Diagnostics;

namespace Prod_em_on_Team3
{
    public class Camera2D
    {
        private float _zoom;
        private float _rotation;
        private Vector2 _position;
        private Matrix _transform = Matrix.Identity;
        private bool _isViewTransformationDirty = true;
        private Matrix _camTranslationMatrix = Matrix.Identity;
        private Matrix _camRotationMatrix = Matrix.Identity;
        private Matrix _camScaleMatrix = Matrix.Identity;
        private Matrix _resTranslationMatrix = Matrix.Identity;

        protected ResolutionIndependentRenderer ResolutionIndependentRenderer;
        private Vector3 _camTranslationVector = Vector3.Zero;
        private Vector3 _camScaleVector = Vector3.Zero;
        private Vector3 _resTranslationVector = Vector3.Zero;

        public Camera2D(ResolutionIndependentRenderer resolutionIndependence)
        {
            ResolutionIndependentRenderer = resolutionIndependence;

            _zoom = 0.1f;
            _rotation = 0.0f;
            _position = Vector2.Zero;
        }

        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                _isViewTransformationDirty = true;
            }
        }

        public void Move(Vector2 amount)
        {
            Position += amount;
        }

        public void SetPosition(Vector2 position)
        {
            Position = position;
        }

        public float Zoom
        {
            get { return _zoom; }
            set
            {
                _zoom = value;
                if (_zoom < 0.1f)
                {
                    _zoom = 0.1f;
                }
                _isViewTransformationDirty = true;
            }
        }

        public float Rotation
        {
            get
            {
                return _rotation;
            }
            set
            {
                _rotation = value;
                _isViewTransformationDirty = true;
            }
        }

        public Matrix GetViewTransformationMatrix()
        {
            if (_isViewTransformationDirty)
            {
                _camTranslationVector.X = -_position.X;
                _camTranslationVector.Y = -_position.Y;

                Matrix.CreateTranslation(ref _camTranslationVector, out _camTranslationMatrix);
                Matrix.CreateRotationZ(_rotation, out _camRotationMatrix);

                _camScaleVector.X = _zoom;
                _camScaleVector.Y = _zoom;
                _camScaleVector.Z = 1;

                Matrix.CreateScale(ref _camScaleVector, out _camScaleMatrix);

                _resTranslationVector.X = ResolutionIndependentRenderer.VirtualWidth * 0.5f;
                _resTranslationVector.Y = ResolutionIndependentRenderer.VirtualHeight * 0.5f;
                _resTranslationVector.Z = 0;

                Matrix.CreateTranslation(ref _resTranslationVector, out _resTranslationMatrix);

                _transform = _camTranslationMatrix *
                             _camRotationMatrix *
                             _camScaleMatrix *
                             _resTranslationMatrix *
                             ResolutionIndependentRenderer.GetTransformationMatrix();

                _isViewTransformationDirty = false;
            }

            return _transform;
        }

        public void RecalculateTransformationMatrices()
        {
            _isViewTransformationDirty = true;
        }
    }
    public class ResolutionIndependentRenderer
    {
        private readonly Game _game;
        private Viewport _viewport;
        private float _ratioX;
        private float _ratioY;
        private Vector2 _virtualMousePosition = new Vector2();

        public Color BackgroundColor = Color.Orange;

        public ResolutionIndependentRenderer(Game game)
        {
            _game = game;
            VirtualWidth = 1366;
            VirtualHeight = 768;

            ScreenWidth = 1024;
            ScreenHeight = 768;
        }

        public int VirtualHeight;

        public int VirtualWidth;

        public int ScreenWidth;
        public int ScreenHeight;

        public void Initialize()
        {
            SetupVirtualScreenViewport();

            _ratioX = (float)_viewport.Width / VirtualWidth;
            _ratioY = (float)_viewport.Height / VirtualHeight;

            _dirtyMatrix = true;
        }

        public void SetupFullViewport()
        {
            var vp = new Viewport();
            vp.X = vp.Y = 0;
            vp.Width = ScreenWidth;
            vp.Height = ScreenHeight;
            _game.GraphicsDevice.Viewport = vp;
            _dirtyMatrix = true;
        }

        public void BeginDraw()
        {
            SetupFullViewport();
            _game.GraphicsDevice.Clear(BackgroundColor);
            SetupVirtualScreenViewport();
        }

        public bool RenderingToScreenIsFinished;
        private static Matrix _scaleMatrix;
        private bool _dirtyMatrix = true;

        public Matrix GetTransformationMatrix()
        {
            if (_dirtyMatrix)
                RecreateScaleMatrix();

            return _scaleMatrix;
        }

        private void RecreateScaleMatrix()
        {
            Matrix.CreateScale((float)ScreenWidth / VirtualWidth, (float)ScreenWidth / VirtualWidth, 1f, out _scaleMatrix);
            _dirtyMatrix = false;
        }

        public Vector2 ScaleMouseToScreenCoordinates(Vector2 screenPosition)
        {
            var realX = screenPosition.X - _viewport.X;
            var realY = screenPosition.Y - _viewport.Y;

            _virtualMousePosition.X = realX / _ratioX;
            _virtualMousePosition.Y = realY / _ratioY;

            return _virtualMousePosition;
        }

        public void SetupVirtualScreenViewport()
        {
            var targetAspectRatio = VirtualWidth / (float)VirtualHeight;
            var width = ScreenWidth;
            var height = (int)(width / targetAspectRatio + .5f);

            if (height > ScreenHeight)
            {
                height = ScreenHeight;
                width = (int)(height * targetAspectRatio + .5f);
            }

            _viewport = new Viewport
            {
                X = (ScreenWidth / 2) - (width / 2),
                Y = (ScreenHeight / 2) - (height / 2),
                Width = width,
                Height = height
            };

            _game.GraphicsDevice.Viewport = _viewport;
        }
    }
}
