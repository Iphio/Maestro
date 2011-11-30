using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Windows;

namespace Maestro
{
    public class Parser
    {
        public Parser()
        {
        }

        public static List<Step> loadStep()
        {
            return null;
        }

        public Song PrepareSongForGame(String filePath)
        {
            //create a song object here (Title, path)
            try
            {

                //Deserialiser le fichier
                XmlSerializer mySerializer = new XmlSerializer(typeof(Song));
                FileStream myFileStream = new FileStream(filePath, FileMode.Open);

                //Return the music
                return ((Song)mySerializer.Deserialize(myFileStream));

               
            }
            catch (IOException)
            {
                MessageBox.Show("Erreur lors de l'ouverture du fichier", "Erreur");
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Le fichier n'est pas une grille", "Mauvais format");
            }

            return null;
        }

        public List<Step> loadStep(String songFile)
        {
            return null;
        }

        public Profile loadProfile(String profileName)
        {
            //create a song object here (Title, path)

            String path = "profiles\\"+profileName+".XML";

            try
            {

                //Deserialiser le fichier
                XmlSerializer mySerializer = new XmlSerializer(typeof(Profile));
                FileStream myFileStream = new FileStream(path, FileMode.Open);

                //Return the music
                return ((Profile)mySerializer.Deserialize(myFileStream));


            }
            catch (IOException)
            {
                MessageBox.Show("Erreur lors de l'ouverture du fichier", "Erreur");
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Le fichier n'est pas une grille", "Mauvais format");
            }
            return null;
        }

        public void saveProfile(Profile p)
        {

            String path = "profiles\\"+ p.name+".XML";

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Profile));
                TextWriter textWriter = new StreamWriter(path);
                serializer.Serialize(textWriter, p);
                textWriter.Close();
            }
            catch (IOException)
            {
                MessageBox.Show("Erreur lors de l'ecriture du fichier", "Erreur");
            
            }

        }

        public void saveSong(Song s, String filePath)
        {

            String test = "C:\\TEST.XML";

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Song));
                TextWriter textWriter = new StreamWriter(@test);
                serializer.Serialize(textWriter, s);
                textWriter.Close();
            }
            catch (IOException)
            {
                MessageBox.Show("Erreur lors de l'ecriture du fichier", "Erreur");

            }
        }
    }
}
