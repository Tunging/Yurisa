using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Algorithm
{
    public class LongestChildArr : BaseManager<LongestChildArr>
    {
        public void Do()
        {
            int[] arr = new int[] { 1, 2, 3, 100, 101, 102, 103, 104, 8, 9, 10 };
            Dictionary<int, List<int>> dict = new Dictionary<int, List<int>>();

            int head = arr[0];
            int _index = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                head++;
                var item = arr[i];
                if (head == item)
                {
                    dict[_index].Add(item);
                }
                else
                {
                    head = item;
                    _index++;
                    dict.Add(_index, new List<int>() { head });
                }
            }

            Console.WriteLine(dict.Count);
        }
    }
}
