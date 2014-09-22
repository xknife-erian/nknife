using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu
{
    public delegate void Action();
    public delegate void Action<T1,T2>(T1 t1,T2 t2);

    public delegate T ActionReturn<T>();
    public delegate TReturn ActionReturn<TReturn,TInput>(TInput t);
}
