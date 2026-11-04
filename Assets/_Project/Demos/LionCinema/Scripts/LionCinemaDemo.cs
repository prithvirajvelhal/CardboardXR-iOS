using CardboardXR.Demos;
using UnityEngine;
using UnityEngine.Rendering;

namespace CardboardXR.Demos.LionCinema
{
    // Procedural seated cinema prototype. Built at runtime so the scene stays small and editable.
    public sealed class LionCinemaDemo : Demo
    {
        Material wall, carpet, seat, gold, screen, trim;
        Transform room;
        public override void OnDemoEnter(CoreServices core) { Build(); }
        public override void OnDemoExit(CoreServices core) { if (room) Destroy(room.gameObject); DestroyMaterials(); }

        void Build()
        {
            room = new GameObject("LION CINEMA | IMAX Auditorium").transform;
            wall = Mat(new Color(.025f,.018f,.035f)); carpet = Mat(new Color(.12f,.012f,.025f));
            seat = Mat(new Color(.34f,.012f,.035f)); gold = Mat(new Color(.72f,.43f,.12f));
            screen = Mat(new Color(.12f,.16f,.22f)); trim = Mat(new Color(.16f,.12f,.09f));
            Make(PrimitiveType.Cube,"Auditorium floor",new Vector3(0,-1.65f,3),new Vector3(16,.25f,22),carpet);
            Make(PrimitiveType.Cube,"Back wall",new Vector3(0,2,13),new Vector3(16,7,.35f),wall);
            Make(PrimitiveType.Cube,"Left wall",new Vector3(-8,2,3),new Vector3(.35f,7,20),wall);
            Make(PrimitiveType.Cube,"Right wall",new Vector3(8,2,3),new Vector3(.35f,7,20),wall);
            Make(PrimitiveType.Cube,"Ceiling",new Vector3(0,5.4f,3),new Vector3(16,.3f,20),wall);
            Make(PrimitiveType.Cube,"Stage",new Vector3(0,-1.15f,7.6f),new Vector3(11,.55f,1.2f),trim);
            BuildCurvedScreen(); BuildSeats(); BuildLights();
            var label = new GameObject("LION CINEMA welcome"); label.transform.SetParent(room,false);
            label.transform.position = new Vector3(0,2.5f,7.1f);
            var text = label.AddComponent<TextMesh>(); text.text="L I O N   C I N E M A"; text.anchor=TextAnchor.MiddleCenter; text.alignment=TextAlignment.Center; text.characterSize=.28f; text.fontSize=64; text.color=new Color(1f,.72f,.32f);
            Debug.Log("[LION CINEMA] Theatre prototype loaded. YouTube texture playback adapter is not yet connected.");
        }
        void BuildCurvedScreen()
        {
            const int segments=48; const float radius=9.2f, angle=1.15f, width=10.2f, height=4.4f;
            var go=new GameObject("Giant curved cinema screen"); go.transform.SetParent(room,false); go.transform.localPosition=new Vector3(0,1.25f,7.3f);
            var mesh=new Mesh(); var v=new Vector3[(segments+1)*2]; var uv=new Vector2[v.Length]; var t=new int[segments*6];
            for(int i=0;i<=segments;i++){float a=-angle/2+angle*i/segments; float x=Mathf.Sin(a)*radius; float z=-Mathf.Cos(a)*radius+radius; v[i*2]=new Vector3(x,-height/2,z); v[i*2+1]=new Vector3(x,height/2,z); uv[i*2]=new Vector2((float)i/segments,0); uv[i*2+1]=new Vector2((float)i/segments,1); if(i<segments){int k=i*6; int b=i*2; t[k]=b;t[k+1]=b+1;t[k+2]=b+2;t[k+3]=b+2;t[k+4]=b+1;t[k+5]=b+3;}}
            mesh.vertices=v;mesh.uv=uv;mesh.triangles=t;mesh.RecalculateNormals(); var mf=go.AddComponent<MeshFilter>();mf.sharedMesh=mesh;var mr=go.AddComponent<MeshRenderer>();mr.material=screen;mr.material.SetColor("_BaseColor",new Color(.035f,.045f,.065f));
            var frame=GameObject.CreatePrimitive(PrimitiveType.Cube);frame.name="Screen surround";frame.transform.SetParent(room,false);frame.transform.localPosition=new Vector3(0,1.25f,7.15f);frame.transform.localScale=new Vector3(width+.5f,height+.5f,.16f);frame.GetComponent<Renderer>().sharedMaterial=gold;
        }
        void BuildSeats(){ for(int row=0;row<4;row++) for(int col=-3;col<=3;col++){float z=-2.2f+row*2.15f;float x=col*1.65f; var baseSeat=Make(PrimitiveType.Cube,$"Seat {row}-{col}",new Vector3(x,-.85f,z),new Vector3(1.05f,.28f,1.0f),trim); Make(PrimitiveType.Cube,"Cushion",new Vector3(x,-.58f,z-.12f),new Vector3(.98f,.28f,.8f),seat);Make(PrimitiveType.Cube,"Backrest",new Vector3(x,-.05f,z+.28f),new Vector3(.98f,1.05f,.28f),seat);Make(PrimitiveType.Cylinder,"Armrest L",new Vector3(x-.52f,-.28f,z-.05f),new Vector3(.12f,.12f,.75f),gold);Make(PrimitiveType.Cylinder,"Armrest R",new Vector3(x+.52f,-.28f,z-.05f),new Vector3(.12f,.12f,.75f),gold); } }
        void BuildLights(){ for(int side=-1;side<=1;side+=2) for(int i=0;i<5;i++){var l=new GameObject("Aisle light").AddComponent<Light>();l.transform.SetParent(room,false);l.transform.localPosition=new Vector3(side*7.2f,-1.35f,-5+i*3.2f);l.type=LightType.Point;l.range=2.5f;l.intensity=.7f;l.color=new Color(1f,.42f,.12f);} }
        GameObject Make(PrimitiveType type,string n,Vector3 p,Vector3 s,Material m){var g=GameObject.CreatePrimitive(type);g.name=n;g.transform.SetParent(room,false);g.transform.localPosition=p;g.transform.localScale=s;g.GetComponent<Renderer>().sharedMaterial=m;return g;}
        Material Mat(Color c){var sh=Shader.Find("Universal Render Pipeline/Lit")??Shader.Find("Standard");var m=new Material(sh);if(m.HasProperty("_BaseColor"))m.SetColor("_BaseColor",c);if(m.HasProperty("_Color"))m.SetColor("_Color",c);return m;}
        void DestroyMaterials(){foreach(var m in new[]{wall,carpet,seat,gold,screen,trim})if(m)Destroy(m);}
    }
}
