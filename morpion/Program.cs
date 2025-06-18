using System.Formats.Asn1;

namespace morpion;

public class Program()
{
    public static string Nom1 = "";
    public static string Nom2 = "";

    public static bool win = false;
    public static bool nomme = true;

    public static bool retry = false;


    public static IDictionary<string, string> Joueurs = new Dictionary<string, string>();

    private static void Main(string[] Args)
    {
        while (nomme)
        {
            nommerJoueur();
            retry = true;
            while (retry)
            {
                win = false;
                PlayAGame();
                Console.WriteLine("1. rejouer\n" +
                "2. Changer de joueur\n" +
                "3. arreter");
                string choix = Console.ReadLine();
                switch (choix)
                {
                    case "1":
                        break;
                    case "2":
                        retry = false;
                        break;
                    case "3":
                        retry = false;
                        nomme = false;
                        break;
                    default:
                        Console.WriteLine("choix invalide ");
                        break;
                }
            }
        }
        
        

    }


    public static void PlayAGame()
    {
        string[,] grille = CreateGrille(3, 3);
        grille = replaceNullBySpace(grille);
        int i = 0;
        while (!win)
        {

            afficherGrille(grille);

            if (i % 2 == 0)
            {
                win = jouer(Nom1, grille);

            }
            else { jouer(Nom2, grille); }
            i += 1;
            if (i == 9 && !win)
            {
                Console.WriteLine("match Nul");
                win = true;
            }
        }
    }


    public static string[,] CreateGrille(int dim1, int dim2)
    {
        string[,] grille = new string[dim1, dim2];




        return grille;
    }


    public static void afficherGrille(string[,] grille)
    {
        Console.WriteLine(" 1   2   3");
        for (int l = 0; l < grille.GetLength(0); l++)
        {
            for (int L = 0; L < grille.GetLength(1); L++)
            {

                if (L == 0) { Console.Write(l + 1 + "  "); }

                Console.Write(grille[l, L]);



                if (!(L == grille.GetLength(1) - 1)) { Console.Write(" | "); }


            }
            Console.WriteLine();
            if (!(l == grille.GetLength(0) - 1))
            {
                Console.WriteLine("   ----------");
            }

        }
    }

    public static string[,] replaceNullBySpace(string[,] grill)
    {
        string[,] grille = grill;
        for (int l = 0; l < grille.GetLength(0); l++)
        {
            for (int L = 0; L < grille.GetLength(1); L++)
            {

                if (grille[l, L] == null)
                {
                    grille[l, L] = " ";
                }
                else
                {
                    grille[l, L] = grille[l, L].Replace("", " ");
                }

            }
        }
        return grille;
    }

    public static int[] convertStringToIntTable(string String)
    {
        string[] tablenumberString = String.Split(" ");
        int[] coordonné = new int[2];
        for (int k = 0; k < tablenumberString.Length; k++)
        {
            coordonné[k] = Convert.ToInt16(tablenumberString[k]);

        }
        return coordonné;
    }


    public static string[,] MettreLePion(string joueur, string[,] grill, int[] coordonnee)
    {
        string symbole = Joueurs[joueur];
        string[,] grille = grill;
        int x = coordonnee[0] - 1;
        int y = coordonnee[1] - 1;
        if (x > 2 || x < 0 || y > 2 || y < 0)
        {
            Console.WriteLine("coordonné invalde, rejouer");
            afficherGrille(grille);
            jouer(joueur, grille);
        }
        else if (!(grille[x, y] == " "))
        {
            Console.WriteLine("rejouer cette case est dejà prise");
            afficherGrille(grille);
            jouer(joueur, grille);
        }
        else
        {
            grille[x, y] = symbole;
        }
        
        return grille;

    }

    public static void nommerJoueur()
    {
        Console.WriteLine("nom joueur 1: ");
        Nom1 = Console.ReadLine();
        Joueurs.Add(Nom1, "X");
        Console.WriteLine("nom joueur 2: ");
        Nom2 = Console.ReadLine();
        Joueurs.Add(Nom2, "O");


    }
    public static bool jouer(string joueur, string[,] grille)
    {
        Console.WriteLine("tour à " + joueur + " , où joue tu (ligne colonne)): ");
        string temp = Console.ReadLine();
        int[] coordonné = convertStringToIntTable(temp);
        
        grille = MettreLePion(joueur, grille, coordonné);
        bool win = verifieWin(coordonné, grille, Joueurs[joueur]);
        if(win){ Console.WriteLine("Bravo " + joueur + " a gagné!"); }
        return win;
    }



    public static bool verifieWin(int[] coordonnee, string[,] grill, string symbol)
    {
        int x = coordonnee[0];
        int y = coordonnee[1];
        string[,] grille = grill;
        bool win = false;
        switch (x)
        {
            case 1:
                switch (y)
                {
                    case 1:
                        if (grille[1, 0] == symbol && grille[2, 0] == symbol || grille[0, 1] == symbol && grille[0, 2] == symbol || grille[1,1] == symbol && grille[2,2] == symbol)
                        {
                            win = true;
                        }
                        break;
                    case 2:
                        if (grille[1, 1] == symbol && grille[2, 1] == symbol || grille[0, 0] == symbol && grille[0, 2] == symbol)
                        {
                            win = true;
                        }
                        break;
                    case 3:
                        if (grille[1, 2] == symbol && grille[2, 2] == symbol || grille[0, 0] == symbol && grille[0, 1] == symbol || grille[1,1] == symbol && grille[0,2] == symbol)
                        {
                            win = true;
                        }
                        break;

                }
                break;
            case 2:
                    switch (y)
                    {
                        case 1:
                            if (grille[0, 0] == symbol && grille[2, 0] == symbol || grille[1, 1] == symbol && grille[1, 2] == symbol)
                            {
                                win = true;
                            }
                            break;
                        case 2:
                            if (grille[0, 1] == symbol && grille[2, 1] == symbol || grille[1, 0] == symbol && grille[1, 2] == symbol || grille[0,0] == symbol && grille[2,2] == symbol || grille[0, 2] == symbol && grille[2, 0] == symbol )
                            {
                                win = true;
                            }
                            break;
                        case 3:
                            if (grille[0, 2] == symbol && grille[2, 2] == symbol || grille[1, 0] == symbol && grille[1, 1] == symbol)
                            {
                                win = true;
                            }
                            break;

                    }
                break;
                case 3:
                    switch (y)
                    {
                        case 1:
                            if (grille[0, 0] == symbol && grille[1, 0] == symbol || grille[2, 1] == symbol && grille[2, 2] == symbol || grille[1, 1] == symbol && grille[0, 2] == symbol)
                            {
                                win = true;
                            }
                            break;
                        case 2:
                            if (grille[0, 1] == symbol && grille[1, 1] == symbol || grille[2, 0] == symbol && grille[2, 2] == symbol)
                            {
                                win = true;
                            }
                            break;
                        case 3:
                            if (grille[0, 2] == symbol && grille[1, 2] == symbol || grille[2, 0] == symbol && grille[2, 1] == symbol || grille[0, 0] == symbol && grille[1, 1] == symbol)
                            {
                                win = true;
                            }
                            break;

                    }
                break;

        }
        return win;
        
    }
}

