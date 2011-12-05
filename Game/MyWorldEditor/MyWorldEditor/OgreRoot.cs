using System;
using System.Collections.Generic;
using Mogre;

namespace MyWorldEditor
{
    public class OgreRoot : Singleton<OgreRoot>
    {
        private OgreRoot()
        {

        }

        public Root root;
        public SceneManager mSceneMgr;

        protected IntPtr hWnd;
        private bool needReDraw = false;

        public void InitMogre()
        {
            //-----------------------------------------------------
            // 1 enter ogre
            //-----------------------------------------------------
            root = new Root();
            //-----------------------------------------------------
            // 2 configure resource paths
            //-----------------------------------------------------
            ConfigFile cf = new ConfigFile();
            cf.Load("../resources.cfg", "\t:=", true);

            // Go through all sections & settings in the file
            ConfigFile.SectionIterator seci = cf.GetSectionIterator();

            String secName, typeName, archName;

            // Normally we would use the foreach syntax, which enumerates the values, but in this case we need CurrentKey too;
            while (seci.MoveNext())
            {
                secName = seci.CurrentKey;
                ConfigFile.SettingsMultiMap settings = seci.Current;
                foreach (KeyValuePair<string, string> pair in settings)
                {
                    typeName = pair.Key;
                    archName = pair.Value;
                    ResourceGroupManager.Singleton.AddResourceLocation(archName, typeName, secName);
                }
            }

            //-----------------------------------------------------
            // 3 Configures the application and creates the window
            //-----------------------------------------------------
            bool foundit = false;
            foreach (RenderSystem rs in root.GetAvailableRenderers())
            {
                root.RenderSystem = rs;
                String rname = root.RenderSystem.Name;
                if (rname == "Direct3D9 Rendering Subsystem")
                {
                    foundit = true;
                    break;
                }
            }

            if (!foundit)
                return;

            root.RenderSystem.SetConfigOption("Full Screen", "No");
            root.RenderSystem.SetConfigOption("Video Mode", "640 x 480 @ 32-bit colour");

            root.RenderSystem.SetConfigOption("VSync", "No");
            root.RenderSystem.SetConfigOption("FSAA", "0");
            //root.RenderSystem.SetConfigOption("NVPerfHUD", "No");
            root.RenderSystem.SetConfigOption("Floating-point mode", "Consistent");


            //IEnumerator<KeyValuePair<String, Mogre.ConfigOption_NativePtr>> map = root.RenderSystem.GetConfigOptions().GetEnumerator();
            //while (map.MoveNext()) 
            //{
            //    LogManager.Singleton.DefaultLog.LogMessage(map.Current.Key);
            //    for (int i = 0; i < map.Current.Value.possibleValues.Count; i++)
            //        LogManager.Singleton.DefaultLog.LogMessage(" = " + map.Current.Value.possibleValues[i]);
            //} 
            root.Initialise(false);
            ResourceGroupManager.Singleton.InitialiseAllResourceGroups();

            mSceneMgr = root.CreateSceneManager(SceneType.ST_GENERIC, "SceneMgr");
            mSceneMgr.AmbientLight = new ColourValue(0.7f, 0.7f, 0.7f);

            Light mLight = mSceneMgr.CreateLight("mainLight");
            mLight.Type = Light.LightTypes.LT_POINT;
            mLight.SetPosition(100, 100, 0);
            mLight.SetDiffuseColour(1.0f, 1.0f, 1.0f);
            mLight.SetSpecularColour(1.0f, 1.0f, 1.0f);            
        }

        public void loadMaterials()
        {
            MaterialPtr guideMtr = MaterialManager.Singleton.Create("RoadGuideRed", ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME);
            guideMtr.ReceiveShadows = false;
            guideMtr.GetTechnique(0).SetLightingEnabled(true);
            guideMtr.GetTechnique(0).GetPass(0).SetDiffuse(1, 0, 0, 0);
            guideMtr.GetTechnique(0).GetPass(0).SetAmbient(1, 0, 0);
            guideMtr.GetTechnique(0).GetPass(0).SetSelfIllumination(1, 0, 0);
            guideMtr.Dispose();

            guideMtr = MaterialManager.Singleton.Create("RoadGuideGreen", ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME);
            guideMtr.ReceiveShadows = false;
            guideMtr.GetTechnique(0).SetLightingEnabled(true);
            guideMtr.GetTechnique(0).GetPass(0).SetDiffuse(0, 1, 0, 0);
            guideMtr.GetTechnique(0).GetPass(0).SetAmbient(0, 1, 0);
            guideMtr.GetTechnique(0).GetPass(0).SetSelfIllumination(0, 1, 0);
            guideMtr.Dispose();

            guideMtr = MaterialManager.Singleton.Create("RoadGuideBlue", ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME);
            guideMtr.ReceiveShadows = false;
            guideMtr.GetTechnique(0).SetLightingEnabled(true);
            guideMtr.GetTechnique(0).GetPass(0).SetDiffuse(0, 0, 1, 0);
            guideMtr.GetTechnique(0).GetPass(0).SetAmbient(0, 0, 1);
            guideMtr.GetTechnique(0).GetPass(0).SetSelfIllumination(0, 0, 1);
            guideMtr.Dispose();

            guideMtr = MaterialManager.Singleton.Create("RoadGuideYellow", ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME);
            guideMtr.ReceiveShadows = false;
            guideMtr.GetTechnique(0).SetLightingEnabled(true);
            guideMtr.GetTechnique(0).GetPass(0).SetDiffuse(1, 1, 0, 0);
            guideMtr.GetTechnique(0).GetPass(0).SetAmbient(1, 1, 0);
            guideMtr.GetTechnique(0).GetPass(0).SetSelfIllumination(1, 1, 0);
            guideMtr.Dispose();

            guideMtr = MaterialManager.Singleton.Create("RoadGray", ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME);
            guideMtr.ReceiveShadows = false;
            guideMtr.GetTechnique(0).SetLightingEnabled(true);
            guideMtr.GetTechnique(0).GetPass(0).SetDiffuse(0.8f, 0.8f, 0.8f, .0f);
            guideMtr.GetTechnique(0).GetPass(0).SetAmbient(0.8f, 0.8f, 0.8f);
            guideMtr.Dispose();

            ManualObject cube = new ManualObject("miniCube");

            cube.Begin("RoadGuideYellow", RenderOperation.OperationTypes.OT_TRIANGLE_STRIP);

            cube.Position(Vector3.ZERO);
            cube.Position(Vector3.UNIT_X);
            cube.Position(Vector3.UNIT_Y);
            cube.Position(Vector3.UNIT_Y + Vector3.UNIT_X);
            cube.Position(Vector3.UNIT_Z);
            cube.Position(Vector3.UNIT_Z + Vector3.UNIT_X);
            cube.Position(Vector3.UNIT_Z + Vector3.UNIT_X + Vector3.UNIT_Y);
            cube.Position(Vector3.UNIT_Z + Vector3.UNIT_Y);

            cube.Index(4); cube.Index(3); cube.Index(7); cube.Index(8); cube.Index(5); cube.Index(3); cube.Index(1); cube.Index(4); cube.Index(2); cube.Index(7); cube.Index(6); cube.Index(5); cube.Index(2); cube.Index(1);

            cube.End();

            cube.CastShadows = false;
            cube.ConvertToMesh("miniCube");
        }

        public SceneNode loadMesh(string name, string fname)
        {
            Entity ent = mSceneMgr.CreateEntity(name, fname);
            SceneNode node = mSceneMgr.RootSceneNode.CreateChildSceneNode(name + "Node");
            node.AttachObject(ent);
            return node;
        }

        private void RenderTimer_Tick(object sender, EventArgs args)
        {
            if (needReDraw)
            {
                root.RenderOneFrame();
                needReDraw = false;
            }
        }

        public void Paint(object sender, EventArgs e)
        {
            root.RenderOneFrame();
        }

        public void Dispose()
        {
            if (root != null)
            {
                root.Dispose();
                root = null;
            }
        }

        //public void createSphere(string strName, float r, uint nSegments, uint nRings = 16)
        //{
        //    MeshPtr pSphere = MeshManager.Singleton.CreateManual(strName, ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME);
        //    SubMesh pSphereVertex = pSphere.CreateSubMesh();

        //    pSphere.sharedVertexData = new VertexData();
        //    VertexData vertexData = pSphere.sharedVertexData;

        //    // define the vertex format
        //    VertexDeclaration vertexDecl = vertexData.vertexDeclaration;
        //    uint currOffset = 0;
        //    // positions
        //    vertexDecl.AddElement(0, currOffset, VertexElementType.VET_FLOAT3, VertexElementSemantic.VES_POSITION);
        //    currOffset += VertexElement.GetTypeSize(VertexElementType.VET_FLOAT3);
        //    // normals
        //    vertexDecl.AddElement(0, currOffset, VertexElementType.VET_FLOAT3, VertexElementSemantic.VES_NORMAL);
        //    currOffset += VertexElement.GetTypeSize(VertexElementType.VET_FLOAT3);
        //    // two dimensional texture coordinates
        //    vertexDecl.AddElement(0, currOffset, VertexElementType.VET_FLOAT2, VertexElementSemantic.VES_TEXTURE_COORDINATES, 0);
        //    currOffset += VertexElement.GetTypeSize(VertexElementType.VET_FLOAT2);

        //    unsafe
        //    {
        //        // allocate the vertex buffer
        //        vertexData.vertexCount = (nRings + 1) * (nSegments + 1);
        //        HardwareVertexBufferSharedPtr vBuf = HardwareBufferManager.Singleton.CreateVertexBuffer(vertexDecl.GetVertexSize(0), vertexData.vertexCount, HardwareBuffer.Usage.HBU_STATIC_WRITE_ONLY, false);
        //        VertexBufferBinding binding = vertexData.vertexBufferBinding;
        //        binding.SetBinding(0, vBuf);
        //        float *pVertex = (float*)(vBuf.Lock(HardwareBuffer.LockOptions.HBL_DISCARD));

        //        // allocate index buffer
        //        pSphereVertex.indexData.indexCount = 6 * nRings * (nSegments + 1);
        //        pSphereVertex.indexData.indexBuffer = HardwareBufferManager.Singleton.CreateIndexBuffer(HardwareIndexBuffer.IndexType.IT_16BIT, pSphereVertex.indexData.indexCount, HardwareBuffer.Usage.HBU_STATIC_WRITE_ONLY, false);
        //        HardwareIndexBufferSharedPtr iBuf = pSphereVertex.indexData.indexBuffer;
        //        uint* pIndices = (uint*)(iBuf.Lock(HardwareBuffer.LockOptions.HBL_DISCARD));

        //        float fDeltaRingAngle = (float)(Mogre.Math.PI / nRings);
        //        float fDeltaSegAngle = (float)(2 * Mogre.Math.PI / nSegments);
        //        uint wVerticeIndex = 0;

        //        // Generate the group of rings for the sphere
        //        for (int ring = 0; ring <= nRings; ring++)
        //        {
        //            float r0 = r * Mogre.Math.Sin(ring * fDeltaRingAngle);
        //            float y0 = r * Mogre.Math.Cos(ring * fDeltaRingAngle);

        //            // Generate the group of segments for the current ring
        //            for (int seg = 0; seg <= nSegments; seg++)
        //            {
        //                float x0 = r0 * Mogre.Math.Sin(seg * fDeltaSegAngle);
        //                float z0 = r0 * Mogre.Math.Cos(seg * fDeltaSegAngle);

        //                // Add one vertex to the strip which makes up the sphere
        //                *pVertex++ = x0;
        //                *pVertex++ = y0;
        //                *pVertex++ = z0;

        //                Vector3 vNormal = new Vector3(x0, y0, z0).NormalisedCopy;
        //                *pVertex++ = vNormal.x;
        //                *pVertex++ = vNormal.y;
        //                *pVertex++ = vNormal.z;

        //                *pVertex++ = (float)seg / (float)nSegments;
        //                *pVertex++ = (float)ring / (float)nRings;

        //                if (ring != nRings)
        //                {
        //                    // each vertex (except the last) has six indices pointing to it
        //                    *pIndices++ = wVerticeIndex + nSegments + 1;
        //                    *pIndices++ = wVerticeIndex;
        //                    *pIndices++ = wVerticeIndex + nSegments;
        //                    *pIndices++ = wVerticeIndex + nSegments + 1;
        //                    *pIndices++ = wVerticeIndex + 1;
        //                    *pIndices++ = wVerticeIndex;
        //                    wVerticeIndex++;
        //                }
        //            }; // end for seg
        //        } // end for ring

        //        // Unlock
        //        vBuf.Unlock();
        //        iBuf.Unlock();
        //        // Generate face list
        //        pSphereVertex.useSharedVertices = true;

        //        // the original code was missing this line:
        //        pSphere._setBounds(new AxisAlignedBox(new Vector3(-r, -r, -r), new Vector3(r, r, r)), false);
        //        pSphere._setBoundingSphereRadius(r);
        //        // this line makes clear the mesh is loaded (avoids memory leaks)
        //        pSphere.Load();
        //    }
        //}
    }
}
