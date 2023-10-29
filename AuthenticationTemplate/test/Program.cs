// See https://aka.ms/new-console-template for more information
using AuthenticationTemplate;

// On sélectionne le type de connexion
char choix = '0';
do
{
    Console.Clear();
    Console.WriteLine("Quel connexion utiliser : ");
    Console.WriteLine("a) Database");
    Console.WriteLine("b) File");

    choix = (char)Console.Read();
} while (choix != 'a' && choix != 'b');

// Chargement du type de connexion
Authenticator auth;
if (choix == 'a')
    auth = new DBAuthenticator("Data Source=../../../../db.db");
else
    auth = new FileAuthenticator("../../../../test.txt");


// On encode le nom d'utilisateur
bool pseudoOk = true;
string username = "";
do
{
    //Console.Clear();
    if (pseudoOk)
        Console.WriteLine("Encodez un pseudo : ");
    else
        Console.WriteLine("Le pseudo n'existe pas, réessayez : ");

    
    flushKeyboard();
    username = Console.ReadLine();
    
    
    if (auth.getLogin(username) == "")
        pseudoOk = false;
    else
        pseudoOk = true;

} while (!pseudoOk);


//On encode le mot de passe
bool pwdOk = true;
do
{
    if (pwdOk)
        Console.WriteLine("Encodez le mdp de " + username + " : ");
    else
        Console.WriteLine("Erreur de mdp pour " + username + " , réessayez : ");


    flushKeyboard();
    string? pwd = Console.ReadLine();
    if (!auth.authenticate(username, pwd))
        pwdOk = false;

} while (!pwdOk);

Console.WriteLine("Connexion valide : " + username);











void flushKeyboard()
{
    while (Console.In.Peek() != -1)
        Console.In.Read();
}