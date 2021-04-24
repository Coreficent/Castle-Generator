namespace Coreficent.Generation
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public interface IAnimatable
    {
        bool HasNext();
        void Next();
    }
}
