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

        public Song loadSSong(String songFile)
        {
            String path = "songs\\" + songFile + ".XML";

            try
            {

                //Deserialiser le fichier
                XmlSerializer mySerializer = new XmlSerializer(typeof(Song));
                FileStream myFileStream = new FileStream(path, FileMode.Open);

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

        public List<Profile> loadProfile()
        {
            //create a song object here (Title, path)

            String path = "profile\\profile.XML";

            try
            {

                //Deserialiser le fichier
                XmlSerializer mySerializer = new XmlSerializer(typeof(List<Profile>));
                FileStream myFileStream = new FileStream(path, FileMode.Open);

                //Return the music
                return ((List<Profile>)mySerializer.Deserialize(myFileStream));


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

        public void saveProfiles(List<Profile> l)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Profile>));
                TextWriter textWriter = new StreamWriter("profile\\profile.XML");
                serializer.Serialize(textWriter, l);
                textWriter.Close();
            }
            catch (IOException)
            {
                MessageBox.Show("Erreur lors de l'ecriture du fichier", "Erreur");
            }

        }

        public void saveSong(Song s, String title)
        {


            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Song));
                TextWriter textWriter = new StreamWriter("songs\\" + title + ".XML");
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
