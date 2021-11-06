using System;

namespace MvvmNavigation.Internal
{
    internal interface IFrameControlTypeProvider
    {
        Type GetFrameControlType(Type navigationManagerType);
    }
}
