using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PluginBase;

namespace WorldEditor
{
    class PluginHost : Singleton<PluginHost>, IPluginHost
    {
        public ISimple SelectedObject { get; set; }   
        public Mogre.Root ORoot { get; private set; }
        public IScene ActiveScene { get; private set; }
        public IMouseListener GlobalMouseListener { get; set; }
        public System.Windows.Forms.Control MainControl { get; private set; }

        public Dictionary<string, IScene> Scenes = new Dictionary<string, IScene>();
        private Types.AvailablePlugins colAvailablePlugins = new Types.AvailablePlugins();

        private PluginHost()
        {
            
        }

        public void Initialize(System.Windows.Forms.Control _MainContrtol = null)
        {
            MainControl = _MainContrtol;
            try
            {
                OgreRoot.Instance.InitMogre();
                ORoot = OgreRoot.Instance.root;
                ActiveScene = createScene();

                UI.Grid.Instance.Init();

                //OgreRoot.Instance.loadMaterials();

                FindPlugins();
            }
            catch (Exception ex)
            {
                CatchException(ex, this);
            }
        }

        public IScene createScene(string name = "")
        {
            IScene scene = new IScene();
            scene.Manager = OgreRoot.Instance.mSceneMgr;
            scene.FloorPlane = new Mogre.Plane(Mogre.Vector3.ZERO, 0);
            Scenes[name] = scene;
            return scene;

        }
        public PluginBase.IPlugin GetCustomPlugin(string name)
        {
            return colAvailablePlugins.Find(name).Instance;
        }

        public void CatchException(Exception e, object sender)
        {
            System.Windows.Forms.MessageBox.Show(e.ToString());
        }

        public void AskRedrow(PluginBase.IPlugin sender)
        {
            OgreRoot.Instance.Paint(sender, null);
        }       

        /// <summary>
        /// A Collection of all Plugins Found and Loaded by the FindPlugins() Method
        /// </summary>
        public Types.AvailablePlugins AvailablePlugins
        {
            get { return colAvailablePlugins; }
            set { colAvailablePlugins = value; }
        }

        /// <summary>
        /// Searches the Application's Startup Directory for Plugins
        /// </summary>
        public void FindPlugins()
        {
            FindPlugins(AppDomain.CurrentDomain.BaseDirectory + @"Plugins\");
        }

        /// <summary>
        /// Searches the passed Path for Plugins
        /// </summary>
        /// <param name="Path">Directory to search for Plugins in</param>
        public void FindPlugins(string Path)
        {
            //First empty the collection, we're reloading them all
            colAvailablePlugins.Clear();

            //Go through all the files in the plugin directory
            foreach (string fileOn in Directory.GetFiles(Path))
            {
                FileInfo file = new FileInfo(fileOn);

                //Preliminary check, must be .dll
                if (file.Extension.Equals(".dll"))
                {
                    //Add the 'plugin'
                    this.AddPlugin(fileOn);
                }
            }
        }

        /// <summary>
        /// Unloads and Closes all AvailablePlugins
        /// </summary>
        public void ClosePlugins()
        {
            foreach (Types.AvailablePlugin pluginOn in colAvailablePlugins)
            {
                //Close all plugin instances
                //We call the plugins Dispose sub first incase it has to do 
                //Its own cleanup stuff
                pluginOn.Instance.Dispose();

                //After we give the plugin a chance to tidy up, get rid of it
                pluginOn.Instance = null;
            }

            //Finally, clear our collection of available plugins
            colAvailablePlugins.Clear();
        }

        public void AddPlugin(string FileName)
        {
            //Create a new assembly from the plugin file we're adding..
            Assembly pluginAssembly = Assembly.LoadFrom(FileName);

            //Next we'll loop through all the Types found in the assembly
            foreach (Type pluginType in pluginAssembly.GetTypes())
            {
                if (colAvailablePlugins.Find(pluginType.Assembly.FullName) == null && pluginType.IsPublic) //Only look at public types
                {
                    if (!pluginType.IsAbstract)  //Only look at non-abstract types
                    {
                        //Gets a type object of the interface we need the plugins to match
                        Type typeInterface = pluginType.GetInterface("PluginBase.IPlugin", true);

                        //Make sure the interface we want to use actually exists
                        if (typeInterface != null)
                        {
                            //Create a new available plugin since the type implements the IPlugin interface
                            Types.AvailablePlugin newPlugin = new Types.AvailablePlugin();

                            //Set the filename where we found it
                            newPlugin.AssemblyPath = FileName;

                            //Create a new instance and store the instance in the collection for later use
                            //We could change this later on to not load an instance.. we have 2 options
                            //1- Make one instance, and use it whenever we need it.. it's always there
                            //2- Don't make an instance, and instead make an instance whenever we use it, then close it
                            //For now we'll just make an instance of all the plugins
                            newPlugin.Instance = (IPlugin)Activator.CreateInstance(pluginAssembly.GetType(pluginType.ToString()));

                            //Set the Plugin's host to this class which inherited IPluginHost
                            newPlugin.Instance.Host = this;

                            //Call the initialization sub of the plugin
                            newPlugin.Instance.Initialize();

                            //Add the new plugin to our collection here
                            this.colAvailablePlugins.Add(newPlugin);

                            //cleanup a bit
                            newPlugin = null;
                        }

                        typeInterface = null; //Mr. Clean			
                    }
                }
            }

            pluginAssembly = null; //more cleanup
        }
       
    }
}
namespace Types
{
    /// <summary>
    /// Collection for AvailablePlugin Type
    /// </summary>
    public class AvailablePlugins : System.Collections.CollectionBase
    {
        //A Simple Home-brew class to hold some info about our Available Plugins

        /// <summary>
        /// Add a Plugin to the collection of Available plugins
        /// </summary>
        /// <param name="pluginToAdd">The Plugin to Add</param>
        public void Add(Types.AvailablePlugin pluginToAdd)
        {
            this.List.Add(pluginToAdd);
        }

        /// <summary>
        /// Remove a Plugin to the collection of Available plugins
        /// </summary>
        /// <param name="pluginToRemove">The Plugin to Remove</param>
        public void Remove(Types.AvailablePlugin pluginToRemove)
        {
            this.List.Remove(pluginToRemove);
        }

        /// <summary>
        /// Finds a plugin in the available Plugins
        /// </summary>
        /// <param name="pluginNameOrPath">The name or File path of the plugin to find</param>
        /// <returns>Available Plugin, or null if the plugin is not found</returns>
        public Types.AvailablePlugin Find(string pluginNameOrPath)
        {
            Types.AvailablePlugin toReturn = null;

            //Loop through all the plugins
            foreach (Types.AvailablePlugin pluginOn in this.List)
            {
                //Find the one with the matching name or filename
                if ((pluginOn.Instance.Name.StartsWith(pluginNameOrPath)) || pluginOn.AssemblyPath.Equals(pluginNameOrPath))
                {
                    toReturn = pluginOn;
                    break;
                }
            }
            return toReturn;
        }
    }

    /// <summary>
    /// Data Class for Available Plugin.  Holds and instance of the loaded Plugin, as well as the Plugin's Assembly Path
    /// </summary>
    public class AvailablePlugin
    {
        //This is the actual AvailablePlugin object.. 
        //Holds an instance of the plugin to access
        //ALso holds assembly path... not really necessary
        private IPlugin myInstance = null;
        private string myAssemblyPath = "";

        public IPlugin Instance
        {
            get { return myInstance; }
            set { myInstance = value; }
        }
        public string AssemblyPath
        {
            get { return myAssemblyPath; }
            set { myAssemblyPath = value; }
        }
    }
}	

