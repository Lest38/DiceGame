using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame
{
    internal class Die
    {
        public int[] Faces { get; }
        public static readonly int FaceNumber = 6;
        public Die(int[] faces)
        {
            if (faces.Length != 6)
                throw new ArgumentException(ValidationError.InvalidFaceCount.ToString());
            Faces = faces;
        }
    }
}
