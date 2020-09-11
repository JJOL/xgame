using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XGame
{
    class XAMLEntity
    {
        private static int UID_COUNT = 0;
        public int UID { get; }


        private float _pX;
        public float pX
        {
            get { return _pX; }
            set { 
                _pX = value;
                draw();
            }
        }
        private float _pY;
        public float pY
        {
            get { return _pY; }
            set { 
                _pY = value;
                draw();
            }
        }

        private float _size;
        public float size {
            get { return _size; }
            set
            {
                _pX = _pX + (_size - value) / 2;
                _pY = _pY + (_size - value) / 2;
                _size = value;

                draw();
            }
        }

        private View _element;
        public View element { get { return _element; } }

        public XAMLEntity(View _element, float size, float x, float y)
        {
            UID = XAMLEntity.UID_COUNT++;


            this._element = _element;
            this._size = size;
            this._pX = x;
            this._pY = y;

            draw();
        }


        private void draw()
        {
            AbsoluteLayout.SetLayoutBounds(
                element,
                new Rectangle(pX, pY, size, size)
            );
        }

        private float currentRotation = 0;
        public void rotate(float degrees)
        {
            currentRotation += degrees;
            element.RotateTo(currentRotation, 2000);
        }


        public bool intersect(XAMLEntity other)
        {
            // Do AABB box intersection
            return (pX < (other.pX + other.size) &&
                    (pX + size) > other.pX &&
                    pY < (other.pY + other.size) &&
                    (pY + size) > other.pY);
        }


        public override bool Equals(object obj)
        {
            return UID == ((XAMLEntity)obj).UID;
        }

        public override int GetHashCode()
        {
            return UID;
        }

    }
}
