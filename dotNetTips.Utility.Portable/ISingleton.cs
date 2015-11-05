using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//TODO: Abstract class too?
namespace dotNetTips.Utility.Portable
{
    internal interface ISingleton<T> where T : class
    {
        //static T Instance { get; }
    }
}