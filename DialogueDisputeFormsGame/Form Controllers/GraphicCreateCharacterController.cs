using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;

using DisputeCommon;
using DialogueCommon.Interfaces;
using DialogueDisputeGameClient;

namespace DialogueDisputeFormsGameForm_Controllers
{
    public class GraphicCreateCharacterController:ICreateCharacterController,
        IDisputeViewController,IDisputeObservable
    {
        CreateCharacterForm myForm;
        List<IControllerObserver> myObservers;

        public GraphicCreateCharacterController()
        {
            myObservers = new List<IControllerObserver>();
        }

        //IDisputeForm implementation
        void IDisputeViewController.start()
        {
            if (myForm == null||myForm.IsDisposed)
            {
                myForm = new CreateCharacterForm(this);//Create and set controller
                myForm.Show();
            }
            else
                myForm.Enabled = true;
        }

        void IDisputeViewController.stop()
        {
            if (this.myForm != null && !this.myForm.IsDisposed)
                this.myForm.Enabled = false;
        }

        void IDisputeViewController.formClosed(string code)
        {
            if (code == "close" || code == "Close")
                notifyObservers("Create Character Form Closed");
        }

        

        //Create Characters Implementation
        /// <summary>
        /// Implements create character by calling the forms save character 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="stats"></param>
        /// <param name="skills"></param>
        /// <param name="atts"></param>
        public void saveCharacter(string name, Dictionary<String, Double> stats, Dictionary<String, Double> skills,
             Dictionary<String, Double> atts)
        {
            object[] data = CharacterData.createSavingData(name, stats, skills, atts);
            

            myForm.SaveCharacterToFile((XmlNode)data[0], (String)data[1],(XmlDocument)data[2]);
            myForm.Close();
             
        }

        //Observable implementation
        /*Observable Stuff*/
        public void registerObserver(object obs)
        {
            myObservers.Add((IControllerObserver)obs);
        }

        public void removeObserver(object obs)
        {
            myObservers.Remove((IControllerObserver)obs);
        }

        public void notifyObservers(object arg=null)
        {
            foreach (IControllerObserver obs in myObservers)
                obs.update((string)arg);
        }
    }
}
