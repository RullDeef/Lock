using System;

namespace Core
{
    [System.Serializable]
    public class Lock
    {
        public Ring[] rings;

        public Lock(int level)
        {
            rings = new Ring[3];
        }
    }

    [System.Serializable]
    public class Ring
    {
        public int offset = 0;
        public int[] numbers = null;

        public int sections
        {
            get
            {
                return numbers.GetLength(0);
            }
        }

        private Ring()
        { }

        public Ring(int offset, int[] numbers)
        {
            this.numbers = numbers.Clone() as int[];
            this.offset = offset;
        }

        public Ring(int[] numbers)
        {
            this.numbers = numbers.Clone() as int[];
            offset = new Random().Next();
        }

        public int At(int position)
        {
            position += offset;

            while (position < 0)
                position += sections;

            while (position > sections)
                position -= sections;

            return numbers[position];
        }

        public Ring Add(Ring ring)
        {
            Ring result = new Ring();
            
            if (sections != ring.sections)
                return null;

            result.numbers = new int[sections];

            for (int i = 0; i < sections; i++)
                result.numbers[i] = At(i) + ring.At(i);

            return result;
        }
    }
}
