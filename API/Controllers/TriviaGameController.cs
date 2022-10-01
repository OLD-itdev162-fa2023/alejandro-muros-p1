using Domain;
using Microsoft.AspNetCore.Mvc;
using Persistence;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class TriviaGameController : ControllerBase
{
    List<int> questionsUsed = new List<int>();

    private static readonly string[] Questions = new[]
    {
        "What is Baby Yoda's real name?", "Where did Obi-Wan take Luke after his birth?",
        "What is Mando''s real name from The Mandalorian?", "Who had the highest midi-chlorian count in Star Wars?",
        "What year did the first Star Wars movie come out?", "What is the name of Boba Fett's ship?",
        "Who are Kylo Ren's parents?", "Who killed Qui-Gon Jinn?", 
        "According to Yoda, there are always how many Sith Lords at a given time?", "What was Finn's stormtrooper number?" 
    };

    private static readonly string[] Answers1 = new string[]
    {
        "Eeth Koth", 
        "Dathomir",
        "Jango Fett", 
        "Anakin Skywalker", 
        "1977", 
        "Millennium Falcon",  
        "Han Solo and Princess Leia", 
        "Count Dooku", 
        "1",  
        "GZ-4619"
    };

    private static readonly string[] Answers2 = new string[]
    {
        "Yoda", 
        "Tatooine", 
        "Cad Bane", 
        "Palpatine", 
        "1985", 
        "Ebon Hawk",  
        "Luke Skywaler and Mara Jade", 
        "Darth Maul", 
        "2",  
        "DA-4239" 
    };

    private static readonly string[] Answers3 = new string[]
    {
        "Grogu", 
        "Mustafar", 
        "Embo", 
        "Obi-Wan", 
        "1966", 
        "Slave 1",  
        "Anakin Skywalker and Padme Amidala", 
        "Palpatine", 
        "3",  
        "BH-1624" 
    };

    private static readonly string[] Answers4 = new string[]
    {
        "Rex", 
        "Naboo", 
        "Din Djarin", 
        "Luke Skywalker", 
        "1980", 
        "Razor Crest",  
        "Boba Fett and Sintas Vel", 
        "Kylo Ren", 
        "4",  
        "FN-2187" 
    };

    private static readonly string[] CorrectAnswers = new[]
    {
        "3", "2", "4", "1", "1", "3", "1", "2", "2", "4"
    };

    private readonly ILogger<TriviaGameController> _logger;

    private readonly DataContext _context;

    public TriviaGameController(ILogger<TriviaGameController> logger, DataContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet(Name = "GetTriviaGame")]
    public IEnumerable<TriviaGame> Get()
    {
        return Enumerable.Range(0, Questions.Length).Select(index => new TriviaGame
        {
            Question = Questions[index],
            Answer1 = Answers1[index],
            Answer2 = Answers2[index],
            Answer3 = Answers3[index],
            Answer4 = Answers4[index],
            CorrectAnswer = CorrectAnswers[index]
        })
        .ToArray();
    }

    [HttpPost]
    public ActionResult<TriviaGame> Create()
    {
        Console.WriteLine($"Database path: {_context.DbPath}");
        Console.WriteLine("Insert a new TriviaGame");

        var trivia = new TriviaGame()
        {
            Question = Questions[3],
            Answer1 = Answers1[3],
            Answer2 = Answers2[3],
            Answer3 = Answers3[3],
            Answer4 = Answers4[3],
            CorrectAnswer = CorrectAnswers[3]
        };

        _context.TriviaGames.Add(trivia);
        var success = _context.SaveChanges() > 0;

        if(success)
        {
            return trivia;
        }

        throw new Exception("Error creating TriviaGame");
    }

    /*public int GetRandomNumber(bool option){
        Random rnd = new Random();
        int chosenNum = rnd.Next(0, Questions.Length);

        if(option == true){
            chosenNum = rnd.Next(0, Questions.Length);

            if(questionsUsed.Contains(chosenNum) == true){
                chosenNum = GetRandomNumber(true);
            }
        }

        else{
            return questionsUsed[questionsUsed.Count - 1];
        }

        questionsUsed.Add(chosenNum);
        return chosenNum;
    }*/
}