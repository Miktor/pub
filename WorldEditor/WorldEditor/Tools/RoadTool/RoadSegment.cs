using Mogre;

namespace WorldEditor.UI.Tools.RoadTool
{
    class RoadSegment
    {
        private void getSubPoints(Vector3 pos,ref Vector3[] arr, int index,Vector3 normal)
        {

        }
        public int[] getVertexes(ControllPoint p1, ControllPoint p2, string name)
        {
          

            //Vector3[] vertexes = new Vector3[]
            //{
            //    p1.HArm.FirstArm.Position,p1.Position,p1.HArm.SecondArm.Position,
            //    p2.HArm.FirstArm.Position,p2.Position,p2.HArm.SecondArm.Position
            //};

            //VertexDeclaration vDec = new VertexDeclaration();
            //vDec.AddElement(0, 0, VertexElementType.VET_FLOAT3, VertexElementSemantic.VES_POSITION);

            //unsafe
            //{
            //    void* voidPtr;
            //    fixed (Vector3* floatPtr = vertexes)
            //    {
            //        voidPtr = floatPtr;
            //        PatchMeshPtr vertex = MeshManager.Singleton.CreateBezierPatch(name, "RoadGroup", voidPtr, vDec, 0, 0, 3, 5);
            //    }                
            //}

            return null;
                     
        }
    }
}
