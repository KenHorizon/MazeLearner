using MazeLearner.Worlds;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.BattleSystems.Questions.English
{
    public class EnglishQuestionBuilder
    {
        //

        //
        public static void Register()
        {
            // Format of making questions
            //EnglishQuestion.Add(Question.Create("Which punctuation ends an exclamation?")
            //    .A(".")
            //    .B("?")
            //    .C("!")
            //    .D(";")
            //    .CorrectQuestion(2));
            EnglishQuestion.Add(Question.Create("Which word is a noun?")
                .A("quickly")
                .B("happiness")
                .C("bright")
                .D("ran")
                .CorrectQuestion(1));
            EnglishQuestion.Add(Question.Create("Which sentence is written correctly?")
                .A("she and I went to the park.")
                .B("She and I went to the park.")
                .C("She and i went to the park.")
                .D("she and i went to the park.")
                .CorrectQuestion(1));
            EnglishQuestion.Add(Question.Create("Choose the correct plural form of \"child.\"")
                .A("childs")
                .B("childrens")
                .C("children")
                .D("childer")
                .CorrectQuestion(2));
            EnglishQuestion.Add(Question.Create("Which word is an adjective?")
                .A("run")
                .B("slowly")
                .C("blue")
                .D("morning")
                .CorrectQuestion(2));
            EnglishQuestion.Add(Question.Create("What is the past tense of \"go\"?")
                .A("goed")
                .B("went")
                .C("gone")
                .D("going")
                .CorrectQuestion(1));
            EnglishQuestion.Add(Question.Create("Which word is an antonym of \"happy\"?")
                .A("joyful")
                .B("sad")
                .C("cheerful")
                .D("pleased")
                .CorrectQuestion(1));
            EnglishQuestion.Add(Question.Create("Choose the correctly punctuated question.")
                .A("What time is it.")
                .B("What time is it?")
                .C("What time is it!")
                .D("What time is it;")
                .CorrectQuestion(1));
            EnglishQuestion.Add(Question.Create("Which sentence uses a contraction?")
                .A("I will finish my homework.")
                .B("I’m going to the store.")
                .C("They are playing outside.")
                .D("He does his chores.")
                .CorrectQuestion(1));
            EnglishQuestion.Add(Question.Create("Which word completes the sentence: \"She has _____ apple.\"")
                .A("a")
                .B("an")
                .C("the")
                .D("some")
                .CorrectQuestion(1));
            EnglishQuestion.Add(Question.Create("Which word is a verb?")
                .A("book")
                .B("quickly")
                .C("jump")
                .D("purple")
                .CorrectQuestion(2));
            EnglishQuestion.Add(Question.Create("Please ____ your hand before you speak.")
                .A("raise")
                .B("rays")
                .C("raze")
                .D("rase")
                .CorrectQuestion(0));
            EnglishQuestion.Add(Question.Create("Which sentence is in the future tense?")
                .A("I ate lunch.")
                .B("I am eating lunch.")
                .C("I will eat lunch.")
                .D("I have eaten lunch.")
                .CorrectQuestion(2));
            EnglishQuestion.Add(Question.Create("Which is a proper noun?")
                .A("city")
                .B("country")
                .C("London")
                .D("school")
                .CorrectQuestion(2));
            EnglishQuestion.Add(Question.Create("Choose the correct possessive form: \"The ____ toy is missing.\"")
                .A("boys")
                .B("boy")
                .C("boy's")
                .D("boys'")
                .CorrectQuestion(2));
            EnglishQuestion.Add(Question.Create("Which sentence uses the correct subject-verb agreement?")
                .A("The dogs chases the cat.")
                .B("The dog chase the cat.")
                .C("The dog chases the cat.")
                .D("The dogs chases the cats.")
                .CorrectQuestion(2));
            EnglishQuestion.Add(Question.Create("Which word is a synonym of \"big\"?")
                .A("tiny")
                .B("large")
                .C("small")
                .D("thin")
                .CorrectQuestion(1));
            EnglishQuestion.Add(Question.Create("Select the correct comparative form of \"fast.\"")
                .A("fastest")
                .B("faster")
                .C("more fast")
                .D("most fast")
                .CorrectQuestion(1));
            EnglishQuestion.Add(Question.Create("Which punctuation mark ends an exclamation?")
                .A(".")
                .B("?")
                .C("!")
                .D(";")
                .CorrectQuestion(2));
            EnglishQuestion.Add(Question.Create("Choose the correct spelling:")
                .A("tommorrow")
                .B("tomorrow")
                .C("tomorow")
                .D("tommorow")
                .CorrectQuestion(1));
            EnglishQuestion.Add(Question.Create("Which sentence contains an adverb?")
                .A("She sang beautifully.")
                .B("She is a singer.")
                .C("The beautiful singer sang.")
                .D("Beautiful songs were sung.")
                .CorrectQuestion(0));
            EnglishQuestion.Add(Question.Create("Which sentence is written in the past continuous tense?")
                .A("She danced yesterday.")
                .B("She was dancing at noon.")
                .C("She will be dancing later.")
                .D("She has danced already.")
                .CorrectQuestion(1));
            EnglishQuestion.Add(Question.Create("Choose the correct article: \"_____ elephant walked by the fence.\"")
                .A("A")
                .B("An")
                .C("The")
                .D("Some")
                .CorrectQuestion(1));
            EnglishQuestion.Add(Question.Create("Which pair are both conjunctions?")
                .A("and, but")
                .B("happy, sad")
                .C("quickly, slowly")
                .D("table, chair")
                .CorrectQuestion(0));
            EnglishQuestion.Add(Question.Create("What is the root word of \"careless\"?")
                .A("care")
                .B("careless")
                .C("less")
                .D("ful")
                .CorrectQuestion(0));
            EnglishQuestion.Add(Question.Create("Which sentence uses direct speech?")
                .A("He said he was hungry.")
                .B("He told that he had eaten.")
                .C("He said, \"I am hungry.\"")
                .D("He thinking about food.")
                .CorrectQuestion(2));
            EnglishQuestion.Add(Question.Create("Choose the correct plural: \"knife\"")
                .A("knifes")
                .B("knives")
                .C("knifeses")
                .D("knievs")
                .CorrectQuestion(1));
            EnglishQuestion.Add(Question.Create("Which sentence shows correct capitalization?")
                .A("my brother went to Paris.")
                .B("My brother went to paris.")
                .C("My brother went to Paris.")
                .D("my Brother went to Paris.")
                .CorrectQuestion(2));
            EnglishQuestion.Add(Question.Create("Which is an interjection?")
                .A("quickly")
                .B("wow")
                .C("before")
                .D("under")
                .CorrectQuestion(1));
            EnglishQuestion.Add(Question.Create("Select the correct reflexive pronoun: \"She did it ____.\"")
                .A("herself")
                .B("hisself")
                .C("myself")
                .D("theirselves")
                .CorrectQuestion(0));
            EnglishQuestion.Add(Question.Create("Which sentence is a question?")
                .A("Please close the door.")
                .B("Did you close the door?")
                .C("Close the door now.")
                .D("You close the door.")
                .CorrectQuestion(1));
            EnglishQuestion.Add(Question.Create("Which word is a compound word?")
                .A("bedroom")
                .B("bed")
                .C("room")
                .D("beds")
                .CorrectQuestion(0));
            EnglishQuestion.Add(Question.Create("Choose the correct order for a sentence: subject + verb + object.")
                .A("Eats John apple.")
                .B("John apple eats.")
                .C("John eats an apple.")
                .D("An apple John eats.")
                .CorrectQuestion(2));
            EnglishQuestion.Add(Question.Create("Which sentence uses passive voice?")
                .A("The chef cooked the meal.")
                .B("The meal was cooked by the chef.")
                .C("The chef is cooking the meal.")
                .D("The chef will cook the meal.")
                .CorrectQuestion(1));
            EnglishQuestion.Add(Question.Create("Which prefix means \"not\"?")
                .A("re-")
                .B("un-")
                .C("pre-")
                .D("post-")
                .CorrectQuestion(1));
            EnglishQuestion.Add(Question.Create("Choose the correct meaning of the idiom: \"break the ice.\"")
                .A("shatter glass")
                .B("begin a conversation")
                .C("make ice cubes")
                .D("break a machine")
                .CorrectQuestion(1));
            EnglishQuestion.Add(Question.Create("Which word is spelled correctly?")
                .A("recieve")
                .B("receive")
                .C("recive")
                .D("receeve")
                .CorrectQuestion(1));
            EnglishQuestion.Add(Question.Create("Which sentence uses an apostrophe correctly for contraction?")
                .A("Its raining outside.")
                .B("It's raining outside.")
                .C("Its' raining outside.")
                .D("It is' raining outside.")
                .CorrectQuestion(1));
            EnglishQuestion.Add(Question.Create("Choose the correct correlative conjunction pair:")
                .A("neither...or")
                .B("both...and")
                .C("either...and")
                .D("not only...or")
                .CorrectQuestion(1));
            EnglishQuestion.Add(Question.Create("Which sentence contains a preposition?")
                .A("The cat slept.")
                .B("The cat slept on the mat.")
                .C("The cat slept loudly.")
                .D("The cat sleeps.")
                .CorrectQuestion(1));
            EnglishQuestion.Add(Question.Create("Which word is an adverb of frequency?")
                .A("rarely")
                .B("red")
                .C("table")
                .D("run")
                .CorrectQuestion(0));
            EnglishQuestion.Add(Question.Create("Choose the correct gerund: \"_____ is fun.\"")
                .A("To swim")
                .B("Swim")
                .C("Swimming")
                .D("Swims")
                .CorrectQuestion(2));
            EnglishQuestion.Add(Question.Create("Which sentence correctly uses quotation marks?")
                .A("She said, \"Let's go to the park.\"")
                .B("She said, \"Lets go to the park.\"")
                .C("She said, Lets go to the park.")
                .D("She said, 'Lets go to the park.\"")
                .CorrectQuestion(0));
            EnglishQuestion.Add(Question.Create("Which sentence is an example of a simile?")
                .A("She is a lion.")
                .B("She runs like the wind.")
                .C("She is brave.")
                .D("She roared loudly.")
                .CorrectQuestion(1));
            EnglishQuestion.Add(Question.Create("Choose the correct antonym of \"difficult.\"")
                .A("easy")
                .B("hard")
                .C("complex")
                .D("tricky")
                .CorrectQuestion(0));
            EnglishQuestion.Add(Question.Create("Which sentence has a comma used correctly?")
                .A("I like apples, and oranges.")
                .B("I like apples and, oranges.")
                .C("I like apples and oranges.")
                .D("I, like apples and oranges.")
                .CorrectQuestion(2));
            EnglishQuestion.Add(Question.Create("Which is the correct past participle of \"write\"?")
                .A("writed")
                .B("wrote")
                .C("written")
                .D("writing")
                .CorrectQuestion(2));
            EnglishQuestion.Add(Question.Create("Which sentence contains a relative clause?")
                .A("The boy who won the race is my friend.")
                .B("The boy won the race.")
                .C("My friend ran.")
                .D("He is tall.")
                .CorrectQuestion(0));
            EnglishQuestion.Add(Question.Create("Choose the correct sentence using \"their.\"")
                .A("Their going to the store.")
                .B("They're going to the store.")
                .C("There going to the store.")
                .D("Their going too the store.")
                .CorrectQuestion(1));
            EnglishQuestion.Add(Question.Create("Which sentence contains an incorrect homophone?")
                .A("I can sea the boat.")
                .B("I can see the boat.")
                .C("She sees the sea.")
                .D("The sea is blue.")
                .CorrectQuestion(0));
            EnglishQuestion.Add(Question.Create("Choose the correct reduction for \"do not\":")
            .A("don't")
            .B("dont")
            .C("do'nt")
            .D("do nott")
            .CorrectQuestion(0));
            EnglishQuestion.Add(Question.Create("Which word is a synonym of \"quiet\"?")
                .A("loud")
                .B("silent")
                .C("noisy")
                .D("bright")
                .CorrectQuestion(1));
            EnglishQuestion.Add(Question.Create("Which sentence uses correct comparative adjectives?")
                .A("She is more tall than him.")
                .B("She is taller than him.")
                .C("She is tallest than him.")
                .D("She is most tall than him.")
                .CorrectQuestion(1));
            EnglishQuestion.Add(Question.Create("Which is the correct order of words in a question?")
                .A("You are going?")
                .B("Are you going?")
                .C("Going are you?")
                .D("You going are?")
                .CorrectQuestion(1));
            EnglishQuestion.Add(Question.Create("Which sentence uses a colon correctly?")
                .A("Bring: pencils, pens, and erasers.")
                .B("Bring pencils: pens, and erasers.")
                .C("Bring the following: pencils, pens, and erasers.")
                .D("Bring pencils, pens: and erasers.")
                .CorrectQuestion(2));
            EnglishQuestion.Add(Question.Create("Choose the correct meaning of \"predict.\"")
                .A("to cook")
                .B("to guess what will happen")
                .C("to forget")
                .D("to repeat")
                .CorrectQuestion(1));
            EnglishQuestion.Add(Question.Create("Which sentence contains a subject pronoun?")
                .A("Give the book to me.")
                .B("I gave the book to her.")
                .C("That book is mine.")
                .D("The book belongs to us.")
                .CorrectQuestion(1));
            EnglishQuestion.Add(Question.Create("Choose the correct suffix that makes nouns from \"child.\"")
                .A("-ed")
                .B("-ing")
                .C("-hood")
                .D("-ly")
                .CorrectQuestion(2));
            EnglishQuestion.Add(Question.Create("Which sentence uses parallel structure?")
                .A("She likes to swim, biking, and to run.")
                .B("She likes swimming, biking, and running.")
                .C("She likes to swim, to bike, and running.")
                .D("She likes swim, bike, run.")
                .CorrectQuestion(1));
            EnglishQuestion.Add(Question.Create("Which question word asks for a reason?")
                .A("Who")
                .B("What")
                .C("Why")
                .D("Where")
                .CorrectQuestion(2));
            EnglishQuestion.Add(Question.Create("Choose the correct form: \"If I ____ rich, I would travel the world.\"")
                .A("am")
                .B("were")
                .C("is")
                .D("will be")
                .CorrectQuestion(1));
            EnglishQuestion.Add(Question.Create("Which sentence shows correct use of \"fewer\" vs \"less\"?")
                .A("There are less apples than yesterday.")
                .B("There are fewer apples than yesterday.")
                .C("There are less water in the glass.")
                .D("There are fewer water in the glass.")
                .CorrectQuestion(1));
            EnglishQuestion.Add(Question.Create("Which is the subject in the sentence: \"The tall girl jumped high.\"")
                .A("tall")
                .B("girl")
                .C("jumped")
                .D("high")
                .CorrectQuestion(1));
            EnglishQuestion.Add(Question.Create("Which sentence contains imagery?")
                .A("The sun was hot and smelled like salt on the sea breeze.")
                .B("The sun was hot.")
                .C("The sun was bright.")
                .D("The sun shone.")
                .CorrectQuestion(0));
            EnglishQuestion.Add(Question.Create("Choose the correct contraction for \"they are\":")
                .A("their")
                .B("there")
                .C("they're")
                .D("theyre")
                .CorrectQuestion(2));
            EnglishQuestion.Add(Question.Create("Which sentence is an example of a command?")
                .A("Would you close the window?")
                .B("Close the window.")
                .C("The window is closed.")
                .D("The window closes.")
                .CorrectQuestion(1));
            EnglishQuestion.Add(Question.Create("Which is the correct order of adjectives?")
                .A("A wooden old small red box.")
                .B("A small old red wooden box.")
                .C("A red small old wooden box.")
                .D("A old small wooden red box.")
                .CorrectQuestion(1));
            EnglishQuestion.Add(Question.Create("Which word is an irregular verb?")
                .A("walk — walked")
                .B("jump — jumped")
                .C("swim — swam")
                .D("talk — talked")
                .CorrectQuestion(2));
            EnglishQuestion.Add(Question.Create("Choose the correct phrase for showing time: \"It is 3:00 ____ the afternoon.\"")
                .A("in")
                .B("on")
                .C("at")
                .D("by")
                .CorrectQuestion(0));
            EnglishQuestion.Add(Question.Create("Which sentence uses a hyphen correctly?")
                .A("The well-known author signed books.")
                .B("The well known author signed books.")
                .C("The well-known author-signed books.")
                .D("The wellknown author signed books.")
                .CorrectQuestion(0));
            EnglishQuestion.Add(Question.Create("Which sentence uses an ellipsis correctly to show trailing off?")
                .A("She whispered, \"I think maybe...\"")
                .B("She whispered, \"I think maybe....\"")
                .C("She whispered, \"I think maybe.\"")
                .D("She whispered, \"I think maybe..\"")
                .CorrectQuestion(0));
            EnglishQuestion.Add(Question.Create("Which word is a pronoun?")
                .A("desk")
                .B("she")
                .C("yellow")
                .D("quickly")
                .CorrectQuestion(1));
            EnglishQuestion.Add(Question.Create("Choose the correct meaning of \"synonym.\"")
                .A("a word with the same meaning")
                .B("a word with opposite meaning")
                .C("a made-up word")
                .D("a very long word")
                .CorrectQuestion(0));
            EnglishQuestion.Add(Question.Create("Which sentence contains an error in tense consistency?")
                .A("She started her homework and finished it.")
                .B("She starts her homework and finished it.")
                .C("She started her homework and is finishing it.")
                .D("She started her homework and has finished it.")
                .CorrectQuestion(1));
            EnglishQuestion.Add(Question.Create("Choose the correct preposition: \"The book is ____ the table.\"")
                .A("in")
                .B("under")
                .C("between")
                .D("beside")
                .CorrectQuestion(1));
            EnglishQuestion.Add(Question.Create("Which sentence is written in present perfect tense?")
                .A("I have eaten breakfast.")
                .B("I ate breakfast.")
                .C("I will eat breakfast.")
                .D("I am eating breakfast.")
                .CorrectQuestion(0));
            EnglishQuestion.Add(Question.Create("Choose the correct homophone: \"I need to ____ a cake for her birthday.\"")
                .A("buy")
                .B("by")
                .C("bye")
                .D("bey")
                .CorrectQuestion(0));
            EnglishQuestion.Add(Question.Create("Which sentence contains a misplaced modifier?")
                .A("Running quickly, the finish line was crossed by Maria.")
                .B("Running quickly, Maria crossed the finish line.")
                .C("Maria, running quickly, crossed the finish line.")
                .D("Maria crossed the finish line, running quickly.")
                .CorrectQuestion(0));
            EnglishQuestion.Add(Question.Create("Which is the correct way to form a question tag: \"You like pizza, ____ ? \"")
                .A("do you")
                .B("don't you")
                .C("will you")
                .D("are you")
                .CorrectQuestion(1));
            EnglishQuestion.Add(Question.Create("Choose the correct sentence with conditional type 1:")
                .A("If it rains, we will stay inside.")
                .B("If it rained, we would stay inside.")
                .C("If it rains, we would stay inside.")
                .D("If it rained, we will stay inside.")
                .CorrectQuestion(0));
            EnglishQuestion.Add(Question.Create("Which sentence uses an ellipsis to indicate omission in a quote?")
                .A("To be, or not to be... that is the question.")
                .B("To be, or not to be, that is the question.")
                .C("To be... or not... to be.")
                .D("To be or not to be...")
                .CorrectQuestion(0));
            EnglishQuestion.Add(Question.Create("Which word is derived from Latin meaning \"letter\"?")
                .A("phon-")
                .B("graph-")
                .C("script-")
                .D("aud-")
                .CorrectQuestion(2));
            EnglishQuestion.Add(Question.Create("Choose the correct direct object: \"Tom kicked the ball.\"")
                .A("Tom")
                .B("kicked")
                .C("the ball")
                .D("none")
                .CorrectQuestion(2));
            EnglishQuestion.Add(Question.Create("Which sentence shows cause and effect?")
                .A("Because he studied, he passed the test.")
                .B("He studied and he passed.")
                .C("He studied but failed.")
                .D("He passed the test.")
                .CorrectQuestion(0));
            EnglishQuestion.Add(Question.Create("Choose the correct tag for reported speech: \"She said she ____ dinner.\"")
                .A("is having")
                .B("has had")
                .C("was having")
                .D("have had")
                .CorrectQuestion(2));
            EnglishQuestion.Add(Question.Create("Which is an example of an oxymoron?")
                .A("deafening silence")
                .B("bright light")
                .C("warm coat")
                .D("fast runner")
                .CorrectQuestion(0));
            EnglishQuestion.Add(Question.Create("Which sentence uses the subjunctive mood correctly?")
                .A("I wish I were taller.")
                .B("I wish I was taller.")
                .C("I wish I will be taller.")
                .D("I wish I am taller.")
                .CorrectQuestion(0));
            EnglishQuestion.Add(Question.Create("Choose the correct infinitive form:")
                .A("To swim")
                .B("Swimming")
                .C("Swam")
                .D("Swum")
                .CorrectQuestion(0));
            EnglishQuestion.Add(Question.Create("Which sentence uses a dative object correctly?")
                .A("She gave him a gift.")
                .B("She gave a gift him.")
                .C("She him gave a gift.")
                .D("She gave gift to him.")
                .CorrectQuestion(0));
            EnglishQuestion.Add(Question.Create("Which punctuation is used to show possession?")
                .A(";")
                .B("'")
                .C(";")
                .D("/")
                .CorrectQuestion(1));
            EnglishQuestion.Add(Question.Create("Which sentence uses the word \"affect\" correctly?")
                .A("The weather will affect our plans.")
                .B("The weather will effect our plans.")
                .C("The affect of the weather was big.")
                .D("The weather had no affect.")
                .CorrectQuestion(0));
            EnglishQuestion.Add(Question.Create("Choose the correct meaning of \"prefix.\"")
                .A("a word ending")
                .B("a word part at the beginning")
                .C("a punctuation mark")
                .D("a type of sentence")
                .CorrectQuestion(1));
            EnglishQuestion.Add(Question.Create("Which sentence includes a plural possessive?")
                .A("The teachers' lounge is upstairs.")
                .B("The teacher's lounge is upstairs.")
                .C("The teachers lounge is upstairs.")
                .D("The teachers lounges are upstairs.")
                .CorrectQuestion(0));
            EnglishQuestion.Add(Question.Create("Which sentence uses correct capitalization for a holiday?")
                .A("we celebrate christmas.")
                .B("We celebrate Christmas.")
                .C("We celebrate christmas.")
                .D("we celebrate Christmas.")
                .CorrectQuestion(1));
            EnglishQuestion.Add(Question.Create("Choose the correct form: \"He is the ____ of the two.\"")
                .A("taller")
                .B("more tall")
                .C("tall")
                .D("tallest")
                .CorrectQuestion(0));
            EnglishQuestion.Add(Question.Create("Which sentence is an example of alliteration?")
                .A("Peter Piper picked a peck of pickled peppers.")
                .B("The sky is blue.")
                .C("She ate an apple.")
                .D("The dog barked.")
                .CorrectQuestion(0));
            EnglishQuestion.Add(Question.Create("Which subordinate conjunction introduces time?")
                .A("although")
                .B("because")
                .C("when")
                .D("unless")
                .CorrectQuestion(2));
            EnglishQuestion.Add(Question.Create("Which sentence shows proper use of quotation marks with punctuation?")
                .A("\"I can't believe it,\" she said.")
                .B("\"I can't believe it\" she said.")
                .C("\"I can't believe it\", she said.")
                .D("\"I can't believe it.\" she said.")
                .CorrectQuestion(0));
            EnglishQuestion.Add(Question.Create("Choose the correct sentence with an appositive:")
                .A("My brother, a doctor, works at the hospital.")
                .B("My brother a doctor works at the hospital.")
                .C("My brother a doctor, works at the hospital.")
                .D("My brother a doctor works, at the hospital.")
                .CorrectQuestion(0));
            EnglishQuestion.Add(Question.Create("Which is the correct order for writing a date in formal writing?")
                .A("September 5, 2026")
                .B("05/09/2026")
                .C("5 September 2026")
                .D("2026 September 5")
                .CorrectQuestion(2));
            EnglishQuestion.Add(Question.Create("Which sentence correctly uses \"among\"?")
                .A("She was chosen among the students.")
                .B("She was chosen between the students.")
                .C("She was chosen in the students.")
                .D("She was chosen among of the students.")
                .CorrectQuestion(0));
        }
    }
}
