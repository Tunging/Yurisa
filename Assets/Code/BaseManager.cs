using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code
{
    public class BaseManager<T> where T:new()
    {
        static T instance;
        public static T Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = Activator.CreateInstance<T>();
                }
                return instance;
            }
        }


    }
}
