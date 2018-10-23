using Prosper.ECS.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prosper.Rendering.MeshRendering
{
    public class MeshComponentFactory : IComponentFactory<MeshComponent>
    {
        public MeshComponent Create()
        {
            return new MeshComponent();
        }
    }
}
