/*Classe Urgence - ici on gère les événements exceptionnels qui peuvent endommager le potager*/
    static class Urgence
    {
        /*Cette méthode est appelée lorsqu'une urgence (comme une grêle ou des rongeurs) survient*/
        public static void GererUrgence()
        {
            Console.WriteLine("\n--- MODE URGENCE ACTIVÉ ---");
            Console.WriteLine("Action d'urgence ?\n1=Faire du bruit\n2=Déployer bâche\n3=Fermer serre\n4=Installer épouvantail\n5=Reboucher trous\n6=Creuser tranchée\n7=Ignorer");
            
            string urgence = Console.ReadLine()!; /*On attend une réponse de l'utilisateur*/

            switch (urgence)
            {
                case "1":
                case "4":
                    Console.WriteLine("🐀 Rongeurs repoussés !"); /*Faire du bruit ou installer un épouvantail est efficace contre les animaux*/
                    break;

                case "2":
                case "3":
                case "5":
                case "6":
                    Console.WriteLine(" Dommages limités par mesure d'urgence."); /*Ces actions aident contre les intempéries*/
                    break;

                default:
                    Console.WriteLine(" Vous n'avez pris aucune mesure ! Des dégâts peuvent survenir...");
                    break;
            }
        }
    }


