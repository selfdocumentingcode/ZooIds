using System;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;

namespace ZooIds
{
    public class ZooIdGenerator
    {
        private readonly Random _random;
        private readonly GeneratorConfig _config;

        private string[] _animals;
        private string[] _adjectives;

        public const int MaxNumAdjectives = 10;

        public ZooIdGenerator()
        {
            _random = new Random();
            _config = GeneratorConfig.Default;

            LoadResources();
        }

        public ZooIdGenerator(int seed)
        {
            _random = new Random(seed);
            _config = GeneratorConfig.Default;

            LoadResources();
        }

        public ZooIdGenerator(GeneratorConfig config)
        {
            _random = new Random();
            _config = config;

            LoadResources();
        }

        public ZooIdGenerator(GeneratorConfig config, int seed)
        {
            _random = new Random(seed);
            _config = config;

            LoadResources();
        }

        private void LoadResources()
        {
            _animals = Resources.Animals.Split(", ");
            _adjectives = Resources.Adjectives.Split(", ");
        }

        public string GenerateId()
        {
            var numAdjectives = _config.NumAdjectives;
            var delimiter = _config.Delimiter;
            var caseStyle = _config.CaseStyle;

            if (numAdjectives > MaxNumAdjectives)
                throw new ArgumentException($"{nameof(numAdjectives)} cannot be greater than {MaxNumAdjectives}");

            var idStringBuilder = new StringBuilder();

            for (int i = 0; i < numAdjectives; i++)
            {
                var randomAdjectiveId = _random.Next(_adjectives.Length);
                var randomAdjective = _adjectives[randomAdjectiveId];

                // Some adjectives are made of two or more words separated by spaces or dashes
                // so remove those and mash into one word
                var sanitizedAdjective = new string(Array.FindAll(randomAdjective.ToCharArray(), char.IsLetter));

                var formattedAdjective = FormatWord(sanitizedAdjective, caseStyle);

                idStringBuilder.Append(formattedAdjective);
                idStringBuilder.Append(delimiter);
            }

            var randomAnimalId = _random.Next(_animals.Length);
            var randomAnimal = FormatWord(_animals[randomAnimalId], caseStyle);

            idStringBuilder.Append(randomAnimal);            

            var generatedId = idStringBuilder.ToString();

            return generatedId;
        }

        private static string FormatWord(string word, CaseStyle caseStyle)
        {
            switch(caseStyle)
            {
                case CaseStyle.UpperCase:
                    return word.ToUpper();

                case CaseStyle.TitleCase:
                    return $"{word[0].ToString().ToUpper()}{word[1..]}";

                case CaseStyle.ToggleCase:
                    var sb = new StringBuilder();

                    for (int i = 0; i < word.Length; i++)
                    {
                        var charString = word[i].ToString();
                        sb.Append(i % 2 == 0 ? charString.ToUpper() : charString);
                    }

                    return sb.ToString();

                case CaseStyle.LowerCase:
                default:
                    return word;
            }
        }
    }
}
