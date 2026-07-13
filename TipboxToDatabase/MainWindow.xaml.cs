using Microsoft.Win32;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using YamlDotNet.Core.Tokens;
using YamlDotNet.RepresentationModel;

public class Card
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string AegisName { get; set; }
}

public class Items
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string AegisName { get; set; }
    public string Type { get; set; }
    public string SubType { get; set; }
    public int EquipLevelMin { get; set; }
    public int Refinable { get; set; }
    public int Gradeable { get; set; }
    public int Location { get; set; }
}

public class Drop
{
    public string AegisName { get; set; }
    public int Rate { get; set; }
    public bool? StealProtected { get; set; }
}

public class Monster
{
    public int Id { get; set; }
    public string AegisName { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public int Hp { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int MagicDefense { get; set; }
    public int Resistence { get; set; }
    public int MagicResistance { get; set; }
    public int Str { get; set; }
    public int Agi { get; set; }
    public int Vit { get; set; }
    public int Int { get; set; }
    public int Dex { get; set; }
    public int Luk { get; set; }
    public string Size { get; set; }
    public string Race { get; set; }
    public string Element { get; set; }
    public List<Drop> MvpDrops { get; set; } = new List<Drop>();
    public List<Drop> Drops { get; set; } = new List<Drop>();
}

public class TipBoxInfo
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Image { get; set; }
    public int Size { get; set; }
    public string Page { get; set; }
    public dbTypes Type { get; set; }
}

public enum dbTypes
{
    MOB,
    CARD,
    ITEM
}

namespace TipboxToDatabase
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        private string num2CardFilePath;

        private string itemInfoFilePath;

        private string mobdbFilePath;

        private string itemdbetcFilePath;

        private string itemdbequipFilePath;

        private string itemdbusableFilePath;

        private string mobspawfolder;

        public MainWindow()
        {
            InitializeComponent();
            if (Properties.Settings.Default.num2CardFilePath != string.Empty)
            {
                num2CardFilePath = Properties.Settings.Default.num2CardFilePath;
                Text.Text = $"{num2CardFilePath}";
            }
            if (Properties.Settings.Default.itemInfoFilePath != string.Empty)
            {
                itemInfoFilePath = Properties.Settings.Default.itemInfoFilePath;
                Text2.Text = $"{itemInfoFilePath}";
            }
            if (Properties.Settings.Default.mobdbFilePath != string.Empty)
            {
                mobdbFilePath = Properties.Settings.Default.mobdbFilePath;
                Text3.Text = $"{mobdbFilePath}";
            }
            if (Properties.Settings.Default.itemdbetcFilePath != string.Empty)
            {
                itemdbetcFilePath = Properties.Settings.Default.itemdbetcFilePath;
                Text4.Text = $"{itemdbetcFilePath}";
            }
            if (Properties.Settings.Default.itemdbequipFilePath != string.Empty)
            {
                itemdbequipFilePath = Properties.Settings.Default.itemdbequipFilePath;
                Text5.Text = $"{itemdbequipFilePath}";
            }
            if (Properties.Settings.Default.itemdbusableFilePath != string.Empty)
            {
                itemdbusableFilePath = Properties.Settings.Default.itemdbusableFilePath;
                Text6.Text = $"{itemdbusableFilePath}";
            }
            if (Properties.Settings.Default.mobspawfolder != string.Empty)
            {
                mobspawfolder = Properties.Settings.Default.mobspawfolder;
                InputFolderPath.Text = $"{mobspawfolder}";
            }
        }

        private void SelectNum2CardFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "num2cardillustnametable.txt (num2cardillustnametable.txt)|num2cardillustnametable.txt"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                num2CardFilePath = openFileDialog.FileName;
                Text.Text = $"{num2CardFilePath}";
                Properties.Settings.Default.num2CardFilePath = num2CardFilePath;
            }
        }

        private void SelectItemInfoFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "ItemInfo files (*.lua;*.lub)|*.lua;*.lub",
                Multiselect = true
            };
            if (openFileDialog.ShowDialog() == true)
            {
                itemInfoFilePath = string.Join("|", openFileDialog.FileNames);
                Text2.Text = $"{itemInfoFilePath}";
                Properties.Settings.Default.itemInfoFilePath = itemInfoFilePath;
            }
        }

        private void SelectMobDBFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "mob_db.yml (mob_db.yml)|mob_db.yml"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                mobdbFilePath = openFileDialog.FileName;
                Text3.Text = $"{mobdbFilePath}";
                Properties.Settings.Default.mobdbFilePath = mobdbFilePath;
            }
        }

        private void SelectItemETCFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "item_db_etc.yml (item_db_etc.yml)|item_db_etc.yml"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                itemdbetcFilePath = openFileDialog.FileName;
                Text4.Text = $"{itemdbetcFilePath}";
                Properties.Settings.Default.itemdbetcFilePath = itemdbetcFilePath;
            }
        }

        private void SelectItemEQUIPFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "item_db_equip.yml (item_db_equip.yml)|item_db_equip.yml"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                itemdbequipFilePath = openFileDialog.FileName;
                Text5.Text = $"{itemdbequipFilePath}";
                Properties.Settings.Default.itemdbequipFilePath = itemdbequipFilePath;
            }
        }

        private void SelectItemUSABLEFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "item_db_usable.yml (item_db_usable.yml)|item_db_usable.yml"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                itemdbusableFilePath = openFileDialog.FileName;
                Text6.Text = $"{itemdbusableFilePath}";
                Properties.Settings.Default.itemdbusableFilePath = itemdbusableFilePath;
            }
        }

        private async void GenerateTipbox_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(num2CardFilePath) ||
                string.IsNullOrEmpty(itemInfoFilePath) ||
                string.IsNullOrEmpty(itemdbetcFilePath) ||
                string.IsNullOrEmpty(itemdbusableFilePath) ||
                string.IsNullOrEmpty(itemdbequipFilePath) ||
                string.IsNullOrEmpty(mobdbFilePath) ||
                string.IsNullOrEmpty(mobspawfolder))
            {
                StatusText.Text = "Please select all files before generating.";
                return;
            }

            // Desabilitar o botão durante o processamento
            ConversionProgressBar.Value = 0;
            ConversionProgressBar.Maximum = 100;
            StatusText.Text = "Generating files please await...";
            (sender as Button).IsEnabled = false;
            Properties.Settings.Default.Save();
            await Task.Delay(500);
            Generate_Tipbox_Main((sender as Button));
        }

        public async void Generate_Tipbox_Main(Button sender)
        {
            StatusText.Text = "Reading Num2CardFile.";
            var num2CardData = ParseNum2CardFile(num2CardFilePath);
            ConversionProgressBar.Value = 17;
            await Task.Delay(200);
            StatusText.Text = "Reading Iteminfo.";
            var itemInfoData = ParseItemInfoFile(itemInfoFilePath);
            ConversionProgressBar.Value = 34;
            await Task.Delay(200);
            StatusText.Text = "Separating Card Info";
            var cardaegis = ParseCardYaml(itemdbetcFilePath);
            ConversionProgressBar.Value = 51;
            await Task.Delay(200);
            var items = ParseItemDBYaml();
            ConversionProgressBar.Value = 68;
            await Task.Delay(200);
            StatusText.Text = "Reading mob_db.yml";
            var mobdbData = ParseMonsterYaml(mobdbFilePath);
            var spawnData = ParseMobsFromFolder(mobspawfolder);
            ConversionProgressBar.Value = 75;
            await Task.Delay(200);
            StatusText.Text = "Building Files.";
            ConversionProgressBar.Value = 90;
            await Task.Delay(200);
            Dictionary<int, TipBoxInfo> TipBoxes = GenerateTipboxFile(num2CardData, itemInfoData, cardaegis, mobdbData, items, spawnData);
            GenerateGuideMonsterLua(TipBoxes, mobdbData, items, spawnData);
            GenerateGuideWeaponLua(TipBoxes, mobdbData, items);
            GenerateGuideArmorLua(TipBoxes, mobdbData, items);
            await Task.Delay(200);
            ConversionProgressBar.Value = 100;
            StatusText.Text = "Files generated successfully.";
            (sender).IsEnabled = true;
        }

        private Dictionary<string, Card> ParseCardYaml(string filePath)
        {
            var cards = new Dictionary<string, Card>();

            // Load the YAML
            using (var reader = new StreamReader(filePath, etcansi.IsChecked == true ? Encoding.GetEncoding(1252) : Encoding.UTF8))
            {
                var yamlStream = new YamlStream();
                yamlStream.Load(reader);

                // Access the root node
                var rootNode = (YamlMappingNode)yamlStream.Documents[0].RootNode;
                var bodyNode = (YamlSequenceNode)rootNode.Children[new YamlScalarNode("Body")];

                // Iterate through each monster node
                foreach (YamlMappingNode cardNode in bodyNode)
                {
                    if (!cardNode.Children.ContainsKey(new YamlScalarNode("Type")) ||
                        cardNode["Type"]?.ToString() != "Card")
                        continue;

                    var card = new Card
                    {
                        Id = cardNode.Children.ContainsKey(new YamlScalarNode("Id"))
                            ? int.Parse(cardNode["Id"].ToString())
                            : 0,
                        Name = cardNode.Children.ContainsKey(new YamlScalarNode("Name"))
                            ? cardNode["Name"]?.ToString()
                            : string.Empty,
                        AegisName = cardNode.Children.ContainsKey(new YamlScalarNode("AegisName"))
                            ? cardNode["AegisName"]?.ToString()
                            : string.Empty
                    };

                    // Add to dictionary
                    if (!string.IsNullOrEmpty(card.AegisName))
                    {
                        if (cards.ContainsKey(card.AegisName))
                            continue;
                        cards[card.AegisName] = card;
                    }
                }
            }

            return cards;
        }

        private Dictionary<string, Items> ParseItemDBYaml()
        {
            var items = new Dictionary<string, Items>();
            string[] filePaths = { itemdbetcFilePath, itemdbequipFilePath, itemdbusableFilePath };
            bool[] ansiChecks = { etcansi.IsChecked == true, equipansi.IsChecked == true, usableansi.IsChecked == true };

            for (int i = 0; i < filePaths.Length; i++)
            {
                var filePath = filePaths[i];
                var encoding = ansiChecks[i] ? Encoding.GetEncoding(1252) : Encoding.UTF8;

                using (var reader = new StreamReader(filePath, encoding))
                {
                    var yamlStream = new YamlStream();
                    yamlStream.Load(reader);

                    // Access the root node
                    var rootNode = (YamlMappingNode)yamlStream.Documents[0].RootNode;
                    var bodyNode = (YamlSequenceNode)rootNode.Children[new YamlScalarNode("Body")];

                    StatusText.Text = $"Reading {System.IO.Path.GetFileName(filePath)}";

                    // Iterate through each item node
                    foreach (YamlMappingNode itemNode in bodyNode)
                    {
                        var item = new Items
                        {
                            Id = itemNode.Children.ContainsKey(new YamlScalarNode("Id"))
                                ? int.Parse(itemNode["Id"].ToString())
                                : 0,
                            Name = itemNode.Children.ContainsKey(new YamlScalarNode("Name"))
                                ? itemNode["Name"]?.ToString()
                                : string.Empty,
                            AegisName = itemNode.Children.ContainsKey(new YamlScalarNode("AegisName"))
                                ? itemNode["AegisName"]?.ToString()
                                : string.Empty,
                            Type = itemNode.Children.ContainsKey(new YamlScalarNode("Type"))
                                ? itemNode["Type"]?.ToString()
                                : string.Empty,
                            SubType = itemNode.Children.ContainsKey(new YamlScalarNode("SubType"))
                                ? itemNode["SubType"]?.ToString()
                                : string.Empty,
                            EquipLevelMin = itemNode.Children.ContainsKey(new YamlScalarNode("EquipLevelMin"))
                                ? int.Parse(itemNode["EquipLevelMin"].ToString())
                                : 0,
                            Refinable = itemNode.Children.ContainsKey(new YamlScalarNode("Refinable")) && itemNode["Refinable"].ToString().ToLower() == "true"? 1 : 0,
                            Gradeable = itemNode.Children.ContainsKey(new YamlScalarNode("Gradeable")) && itemNode["Gradeable"].ToString().ToLower() == "true"? 1 : 0
                        };

                        if (itemNode.Children.ContainsKey(new YamlScalarNode("Locations")))
                        {
                            var locNode = itemNode["Locations"] as YamlMappingNode;

                            foreach (var loc in locNode.Children)
                            {
                                string locationName = loc.Key.ToString();
                                bool value = bool.Parse(loc.Value.ToString());

                                if (value && locationMap.TryGetValue(locationName, out int locId))
                                {
                                    item.Location = locId;
                                    break; // pegamos a primeira localização válida
                                }
                            }
                        }
                        else
                        {
                            item.Location = -1; // sem localização
                        }

                        if (!string.IsNullOrEmpty(item.AegisName))
                        {
                            if (items.ContainsKey(item.AegisName))
                                continue;
                            items[item.AegisName] = item;
                        }
                    }
                }
            }

            return items;
        }

        private Dictionary<int, Monster> ParseMonsterYaml(string filePath)
        {
            var monsters = new Dictionary<int, Monster>();
            Monster currentMonster = null;

            // Ler todas as linhas do arquivo em uma lista
            var lines = File.ReadAllLines(filePath, mobansi.IsChecked == true ? Encoding.GetEncoding(1252) : Encoding.UTF8);

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i].Trim(); // Limpar espaços em branco

                // Ignorar linhas que começam com #
                if (line.StartsWith("#"))
                    continue;

                // Verificar se a linha contém um novo monstro
                if (line.StartsWith("- Id:"))
                {
                    // Se já houver um monstro atual, adicione-o ao dicionário
                    if (currentMonster != null && currentMonster.Id != 0)
                    {
                        if (!monsters.ContainsKey(currentMonster.Id))
                        {
                            monsters.Add(currentMonster.Id, currentMonster);
                        }
                    }

                    // Criar um novo objeto Monster
                    currentMonster = new Monster();

                    // Extrair o ID do monstro
                    currentMonster.Id = int.Parse(line.Substring(line.IndexOf(':') + 1).Trim());
                }
                else if (currentMonster != null)
                {
                    // Processar outras propriedades
                    if (line.StartsWith("AegisName:"))
                    {
                        currentMonster.AegisName = line.Substring(line.IndexOf(':') + 1).Trim();
                    }
                    else if (line.StartsWith("Name:"))
                    {
                        currentMonster.Name = line.Substring(line.IndexOf(':') + 1).Trim();
                    }
                    else if (line.StartsWith("Level:"))
                    {
                        currentMonster.Level = int.Parse(line.Substring(line.IndexOf(':') + 1).Trim());
                    }
                    else if (line.StartsWith("Hp:"))
                    {
                        currentMonster.Hp = int.Parse(line.Substring(line.IndexOf(':') + 1).Trim());
                    }
                    else if (line.StartsWith("Attack:"))
                    {
                        currentMonster.Attack = int.Parse(line.Substring(line.IndexOf(':') + 1).Trim());
                    }
                    else if (line.StartsWith("Defense:"))
                    {
                        currentMonster.Defense = int.Parse(line.Substring(line.IndexOf(':') + 1).Trim());
                    }
                    else if (line.StartsWith("MagicDefense:"))
                    {
                        currentMonster.MagicDefense = int.Parse(line.Substring(line.IndexOf(':') + 1).Trim());
                    }
                    else if (line.StartsWith("Str:"))
                    {
                        currentMonster.Str = int.Parse(line.Substring(line.IndexOf(':') + 1).Trim());
                    }
                    else if (line.StartsWith("Agi:"))
                    {
                        currentMonster.Agi = int.Parse(line.Substring(line.IndexOf(':') + 1).Trim());
                    }
                    else if (line.StartsWith("Vit:"))
                    {
                        currentMonster.Vit = int.Parse(line.Substring(line.IndexOf(':') + 1).Trim());
                    }
                    else if (line.StartsWith("Int:"))
                    {
                        currentMonster.Int = int.Parse(line.Substring(line.IndexOf(':') + 1).Trim());
                    }
                    else if (line.StartsWith("Dex:"))
                    {
                        currentMonster.Dex = int.Parse(line.Substring(line.IndexOf(':') + 1).Trim());
                    }
                    else if (line.StartsWith("Luk:"))
                    {
                        currentMonster.Luk = int.Parse(line.Substring(line.IndexOf(':') + 1).Trim());
                    }
                    else if (line.StartsWith("Size:"))
                    {
                        currentMonster.Size = line.Substring(line.IndexOf(':') + 1).Trim();
                    }
                    else if (line.StartsWith("Race:"))
                    {
                        currentMonster.Race = line.Substring(line.IndexOf(':') + 1).Trim();
                    }
                    else if (line.StartsWith("Element:"))
                    {
                        currentMonster.Element = line.Substring(line.IndexOf(':') + 1).Trim();
                    }
                    else if (line.StartsWith("MvpDrops:"))
                    {
                        // Iniciar a leitura dos drops
                        i++; // Avança para a próxima linha
                        while (i < lines.Length && !lines[i].Trim().StartsWith("Drops:")) // Enquanto não encontrar um novo monstro
                        {
                            line = lines[i].Trim();

                            // Se a linha não estiver vazia e começar com um item
                            if (!string.IsNullOrWhiteSpace(line) && line.StartsWith("- Item:"))
                            {
                                var drop = new Drop
                                {
                                    AegisName = line.Substring(line.IndexOf(':') + 1).Trim(),
                                    StealProtected = false // Valor padrão
                                };

                                // Adicionar o drop ao monstro
                                currentMonster.MvpDrops.Add(drop);
                            }
                            else if (!string.IsNullOrWhiteSpace(line) && line.StartsWith("Rate:"))
                            {
                                currentMonster.MvpDrops[currentMonster.MvpDrops.Count - 1].Rate = int.Parse(line.Substring(line.IndexOf(':') + 1).Trim());
                            }

                            // Apenas avança se não ultrapassar os limites
                            if (i + 1 < lines.Length)
                            {
                                i++; // Avança para a próxima linha
                            }
                            else
                            {
                                break; // Sai do loop se chegar ao fim do arquivo
                            }
                        }

                        // Ajuste para o loop principal não pular a próxima linha
                        i--;
                    }
                    else if (line.StartsWith("Drops:"))
                    {
                        // Iniciar a leitura dos drops
                        i++; // Avança para a próxima linha
                        while (i < lines.Length && !lines[i].Trim().StartsWith("- Id:")) // Enquanto não encontrar um novo monstro
                        {
                            line = lines[i].Trim();

                            // Se a linha não estiver vazia e começar com um item
                            if (!string.IsNullOrWhiteSpace(line) && line.StartsWith("- Item:"))
                            {
                                var drop = new Drop
                                {
                                    AegisName = line.Substring(line.IndexOf(':') + 1).Trim(),
                                    StealProtected = false // Valor padrão
                                };

                                // Adicionar o drop ao monstro
                                currentMonster.Drops.Add(drop);
                            }
                            else if (!string.IsNullOrWhiteSpace(line) && line.StartsWith("Rate:"))
                            {
                                currentMonster.Drops[currentMonster.Drops.Count - 1].Rate = int.Parse(line.Substring(line.IndexOf(':') + 1).Trim());
                            }

                            // Apenas avança se não ultrapassar os limites
                            if (i + 1 < lines.Length)
                            {
                                i++; // Avança para a próxima linha
                            }
                            else
                            {
                                break; // Sai do loop se chegar ao fim do arquivo
                            }
                        }

                        // Ajuste para o loop principal não pular a próxima linha
                        i--;
                    }
                }
            }

            // Adiciona o último monstro lido
            if (currentMonster != null && currentMonster.Id != 0)
            {
                if (!monsters.ContainsKey(currentMonster.Id))
                {
                    monsters.Add(currentMonster.Id, currentMonster);
                }
            }

            return monsters;
        }

        private Dictionary<int, string> ParseNum2CardFile(string filePath)
        {
            var data = new Dictionary<int, string>();
            var lines = File.ReadAllLines(filePath, nun2ansi.IsChecked == true ? Encoding.GetEncoding(1252) : Encoding.UTF8);

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("//")) continue;
                var parts = line.Split('#');
                if (parts.Length >= 3 && int.TryParse(parts[0], out int id))
                {
                    data[id] = parts[1];
                }
            }

            return data;
        }

        private Dictionary<int, (string Title, string Resource, List<string> Description)> ParseItemInfoFile(string filePaths)
        {
            var data = new Dictionary<int, (string, string, List<string>)>();
            string[] paths = filePaths.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var filePath in paths)
            {
                if (!File.Exists(filePath)) continue;

                var content = File.ReadAllText(filePath, iiansi.IsChecked == true ? Encoding.GetEncoding(1252) : Encoding.UTF8);

                // Updated regex to match items with nested braces
                var itemPattern = new Regex(@"\[(\d+)]\s*=\s*\{((?:[^{}]*\{[^{}]*\})*[^{}]*)\}", RegexOptions.Singleline);

                foreach (Match match in itemPattern.Matches(content))
                {
                    int id = int.Parse(match.Groups[1].Value); // Extract the item ID
                    if (data.ContainsKey(id))
                        continue;

                    string itemData = match.Groups[2].Value;  // Extract the entire item data block

                    // Extract `identifiedDisplayName`
                    var titleMatch = Regex.Match(itemData, @"\tidentifiedDisplayName\s*=\s*""(.*?)""");
                    string title = titleMatch.Success ? titleMatch.Groups[1].Value : string.Empty;

                    // Extract `identifiedResourceName`
                    var resourceMatch = Regex.Match(itemData, @"\tidentifiedResourceName\s*=\s*""(.*?)""");
                    string resource = resourceMatch.Success ? resourceMatch.Groups[1].Value : string.Empty;

                    // Extract `identifiedDescriptionName`
                    var descriptionMatch = Regex.Match(itemData, @"\tidentifiedDescriptionName\s*=\s*\{(.*?)\}", RegexOptions.Singleline);
                    string descriptionBlock = descriptionMatch.Success ? descriptionMatch.Groups[1].Value : string.Empty;

                    var descriptions = new List<string>();
                    if (!string.IsNullOrEmpty(descriptionBlock))
                    {
                        // Split and clean each description line
                        foreach (var desc in descriptionBlock.Split(new[] { "\r", "\n", "," }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            string trimmed = desc.Trim(' ', '"', '\t').Replace("\\", "").Replace("\\\"", "\""); // Handle escaped quotes
                            if (!string.IsNullOrEmpty(trimmed))
                                descriptions.Add(trimmed);
                        }
                    }

                    // Add the parsed data to the dictionary
                    data[id] = (title, resource, descriptions);
                }
            }

            return data;
        }

        private void WriteEndTipbox(StreamWriter writer)
        {
            writer.WriteLine("function main()");
            writer.WriteLine("\tfor DocumentNum, INFO in pairs(tbl) do");
            writer.WriteLine("\t\tresult, msg = AddTip(DocumentNum, INFO.Title, INFO.Search, INFO.Image)");
            writer.WriteLine("\t\tif not result then");
            writer.WriteLine("\t\t\treturn false, msg");
            writer.WriteLine("\t\tend");
            writer.WriteLine("\t\tresult, msg = AddImgcoord(DocumentNum, INFO.Imgcoord.Position, INFO.Imgcoord.Size)");
            writer.WriteLine("\t\tif not result then");
            writer.WriteLine("\t\t\treturn false, msg");
            writer.WriteLine("\t\tend");
            writer.WriteLine("\t\tif not INFO.PageEX then");
            writer.WriteLine("\t\t\tresult, msg = AddPageEx(DocumentNum, -1, 0, 0, 0, 0, 0, 0, 0)");
            writer.WriteLine("\t\t\tif not result then");
            writer.WriteLine("\t\t\t\treturn false, msg");
            writer.WriteLine("\t\t\tend");
            writer.WriteLine("\t\telse");
            writer.WriteLine("\t\t\tfor k, v in pairs(INFO.PageEX) do");
            writer.WriteLine("\t\t\t\tresult, msg = AddPageEx(DocumentNum, v.EffectNum, v.Twinkle, v.StartX, v.StartY, v.DistX, v.DistY, v.MoveTime)");
            writer.WriteLine("\t\t\t\tif not result then");
            writer.WriteLine("\t\t\t\t\treturn false, msg");
            writer.WriteLine("\t\t\t\tend");
            writer.WriteLine("\t\t\tend");
            writer.WriteLine("\t\tend");
            writer.WriteLine("\t\tfor k, v in pairs(INFO.Page) do");
            writer.WriteLine("\t\t\tresult, msg = AddPage(DocumentNum, v)");
            writer.WriteLine("\t\t\tif not result then");
            writer.WriteLine("\t\t\t\treturn false, msg");
            writer.WriteLine("\t\t\tend");
            writer.WriteLine("\t\tend");
            writer.WriteLine("\tend");
            writer.WriteLine("\treturn true, \"good\"");
            writer.WriteLine("end");
        }

        private void SelectFolderButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                CheckFileExists = false,
                CheckPathExists = true,
                FileName = "Selecione a pasta",
                Filter = "Pastas|*.none", // Truque para que a interface não mostre arquivos
                Title = "Selecione a pasta de mobs"
            };

            if (dialog.ShowDialog() == true)
            {
                string selectedPath = Path.GetDirectoryName(dialog.FileName);
                InputFolderPath.Text = selectedPath; // Atualiza o TextBox com o caminho da pasta
                mobspawfolder = selectedPath;
                Properties.Settings.Default.mobspawfolder = selectedPath;
            }
        }

        private Dictionary<int, Dictionary<string, int>> ParseMobsFromFolder(string folderPath)
        {
            var mobDictionary = new Dictionary<int, Dictionary<string, int>>();

            if (!Directory.Exists(folderPath))
            {
                throw new DirectoryNotFoundException("A pasta especificada não foi encontrada.");
            }

            var txtFiles = Directory.GetFiles(folderPath, "*.txt", SearchOption.AllDirectories);

            foreach (var file in txtFiles)
            {
                var lines = File.ReadAllLines(file);

                foreach (var line in lines)
                {
                    // Ignorar linhas de comentários ou linhas vazias
                    if (string.IsNullOrWhiteSpace(line) || line.StartsWith("//") || line.StartsWith("#"))
                    {
                        continue;
                    }

                    // Regex para capturar os dados
                    var match = Regex.Match(line,
                        @"^(?<map>\S+)(?:,(?<x>\d+)(?:,(?<y>\d+)(?:,(?<xs>\d+)(?:,(?<ys>\d+))?)?)?)?\s+(?<type>monster|bossmonster)\s+(?<name>[^\t]+)\s+(?<id>\d+),(?<amount>\d+)",
                        RegexOptions.IgnoreCase);

                    if (match.Success)
                    {
                        string map = match.Groups["map"].Value;
                        var temp = map.Split(',');
                        map = temp[0];
                        int id = int.Parse(match.Groups["id"].Value);
                        int amount = int.Parse(match.Groups["amount"].Value);

                        if (!mobDictionary.ContainsKey(id))
                        {
                            mobDictionary[id] = new Dictionary<string, int>();
                        }

                        if (!mobDictionary[id].ContainsKey(map))
                        {
                            mobDictionary[id][map] = 0;
                        }

                        mobDictionary[id][map] += amount;
                    }
                }
            }

            return mobDictionary;
        }

        private Dictionary<int, TipBoxInfo> GenerateTipboxFile(Dictionary<int, string> num2CardData, Dictionary<int, (string Title, string Resource, List<string> Description)> itemInfoData, Dictionary<string, Card> carddb, Dictionary<int, Monster> mobdb, Dictionary<string, Items> itemdb, Dictionary<int, Dictionary<string, int>> spawnData)
        {
            Dictionary<int, TipBoxInfo> TipBoxDB = new Dictionary<int, TipBoxInfo>();
            foreach (var id in mobdb.Keys)
            {
                if (mobdb.TryGetValue(id, out var mobinfo))
                {
                    foreach (var drop in mobinfo.Drops)
                    {
                        if (carddb.TryGetValue(drop.AegisName, out var thiscard))
                        {
                            if (num2CardData.TryGetValue(thiscard.Id, out var cinfo))
                            {
                                foreach (var mob in spawnData)
                                {
                                    if (mob.Key == mobinfo.Id)
                                    {
                                        TipBoxInfo Info = new TipBoxInfo();
                                        string page = string.Empty;
                                        page += $"Id: {mobinfo.Id}\\nName: {mobinfo.Name}\\nLevel: {mobinfo.Level}\\nHp: {mobinfo.Hp}\\nAttack: {mobinfo.Attack}\\nDefense: {mobinfo.Defense}\\nMagicDefense: {mobinfo.MagicDefense}\\nResistence: {mobinfo.Resistence}\\nMagicResistence: {mobinfo.Level}\\nStr: {mobinfo.Str}\\nVit: {mobinfo.Vit}\\nAgi: {mobinfo.Agi}\\nInt: {mobinfo.Int}\\nDex: {mobinfo.Dex}\\nLuk: {mobinfo.Luk}\\nSize: {mobinfo.Size}\\nRace: {mobinfo.Race}\\nElement: {mobinfo.Element}";
                                        if (mobinfo.MvpDrops.Count > 0)
                                        {
                                            page += $"\\nMVPDrops:";
                                            foreach (var d in mobinfo.MvpDrops)
                                            {
                                                if (itemdb.TryGetValue(d.AegisName, out var it))
                                                {
                                                    string dropdisplay = $"<ITEM>{it.Name}<INFO>{it.Id}</INFO></ITEM>";
                                                    page += $"\\n{dropdisplay}  - {d.Rate / 100.0:F2}%";
                                                }
                                            }
                                        }
                                        if (mobinfo.Drops.Count > 0)
                                        {
                                            page += $"\\nDrops:";
                                            foreach (var d in mobinfo.Drops)
                                            {
                                                if (itemdb.TryGetValue(d.AegisName, out var it))
                                                {
                                                    string dropdisplay = $"<ITEM>{it.Name} [{it.Id}]<INFO>{it.Id}</INFO></ITEM>";
                                                    page += $"\\n - {dropdisplay}  - {d.Rate / 100.0:F2}%";
                                                }
                                            }
                                        }

                                        page += "\\nCan be found on:";
                                        foreach (var mapData in mob.Value)
                                        {
                                            page += $"\\n<NAVI>Map: {mapData.Key}, Amount: {mapData.Value}<INFO>{mapData.Key},0,0,0,000,0</INFO></NAVI>";
                                        }

                                        Info.Id = id;
                                        Info.Title = $"{mobinfo.Name} [{mobinfo.Id}]";
                                        Info.Image = $"À¯ÀúÀÎÅÍÆäÀÌ½º\\\\cardbmp\\\\{cinfo}";
                                        Info.Size = 100;
                                        Info.Page = page;
                                        Info.Type = dbTypes.MOB;
                                        TipBoxDB[id] = Info;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            int count = 1;

            foreach (var id in itemInfoData.Keys)
            {
                while (TipBoxDB.TryGetValue(count, out var tbdb))
                    count++;
                TipBoxInfo Info = new TipBoxInfo();
                if (itemInfoData.TryGetValue(id, out var itemInfo))
                {
                    if (dropable_only.IsChecked == true)
                    {
                        bool isDropable = mobdb.Values.Any(mob => mob.Drops.Any(drop => itemdb.TryGetValue(drop.AegisName, out var it) && it.Id == id));
                        if (!isDropable)
                            continue;
                    }

                    Info.Id = id;
                    Info.Title = $"{itemInfo.Title} [{id}]";
                    if (num2CardData.TryGetValue(id, out var cardinfo))
                    {
                        Info.Type = dbTypes.CARD;
                        Info.Image = $"À¯ÀúÀÎÅÍÆäÀÌ½º\\\\cardbmp\\\\{num2CardData[id]}";
                        Info.Size = 100;
                    }
                    else
                    {
                        Info.Type = dbTypes.ITEM;
                        Info.Image = $"À¯ÀúÀÎÅÍÆäÀÌ½º\\\\collection\\\\{itemInfo.Resource}";
                        Info.Size = 200;
                    }
                    Info.Page = $"ItemID:[{id}]\\n_______________________\\n{string.Join("\\n", itemInfo.Description.Select(d => d.Replace("\n", "\\n").Replace("\r", "").Replace("\"", "").Trim()))}";
                    bool is_dropped = false;
                    Parallel.ForEach(mobdb.Keys, mobid =>
                    {
                        if (mobdb.TryGetValue(mobid, out var mobinfo))
                        {
                            foreach (var mob in spawnData)
                            {
                                if (mob.Key == mobinfo.Id)
                                {
                                    if (mobinfo.Drops.Count > 0)
                                    {
                                        foreach (var d in mobinfo.Drops) // Inner loop doesn't need parallelization
                                        {
                                            if (itemdb.TryGetValue(d.AegisName, out var it))
                                            {
                                                if (it.Id != id)
                                                    continue;

                                                lock (Info) // Synchronize access to shared resource
                                                {
                                                    if (!is_dropped)
                                                    {
                                                        is_dropped = true;
                                                        Info.Page += $"\\nMonster drops:";
                                                    }
                                                    Info.Page += $"\\n<TIPBOX>{mobinfo.Name}<INFO>{mobinfo.Id}</INFO></TIPBOX>";
                                                }
                                            }
                                        }
                                    }
                                }
                                break;
                            }
                        }
                    });

                    TipBoxDB[count] = Info;
                    count++;
                }
            }

            using (var writer = new StreamWriter("tipbox.lub", false, tipansi.IsChecked == true ? Encoding.GetEncoding(1252) : Encoding.UTF8))
            {
                writer.WriteLine("tbl = {");

                // Sort the dictionary by ID (keys)
                var sortedKeys = TipBoxDB.Keys.OrderBy(id => id).ToList();

                var lastKey = sortedKeys.Last();
                string buffer = string.Empty;
                foreach (var id in sortedKeys)
                {
                    if (TipBoxDB.TryGetValue(id, out var tipInfo))
                    {
                        writer.WriteLine($"\t[{id}] = {{");
                        writer.WriteLine($"\t\tTitle = \"{tipInfo.Title}\",");
                        writer.WriteLine("\t\tSearch = 1,");
                        writer.WriteLine($"\t\tImage = \"{tipInfo.Image}\",");
                        writer.WriteLine($"\t\tImgcoord = {{ Position = 5, Size = {tipInfo.Size} }},");
                        writer.WriteLine("\t\tPage = {");
                        writer.WriteLine($"\t\t\t\"{tipInfo.Page}\"");
                        writer.WriteLine("\t\t}");
                        string lastword = id.Equals(lastKey) ? string.Empty : ",";
                        writer.WriteLine($"\t}}{lastword}");
                    }
                }
                writer.WriteLine(buffer.TrimEnd('\n'));
                writer.WriteLine("}");
                WriteEndTipbox(writer);
            }

            return TipBoxDB;
        }

        private void GenerateGuideMonsterLua(Dictionary<int, TipBoxInfo> TipBoxes,Dictionary<int, Monster> mobdb, Dictionary<string, Items> itemdb, Dictionary<int, Dictionary<string, int>> spawnData)
        {
            // Ordena monstros por level asc (se empate, por Id)
            var mobs = mobdb.Values
                .OrderBy(m => m.Level)
                .ThenBy(m => m.Id)
                .ToList();

            using (var sw = new StreamWriter("guide_monster.lub", false, tipansi.IsChecked == true ? Encoding.GetEncoding(1252) : Encoding.UTF8))
            {
                sw.WriteLine("GuideMonster.TitleText = \"Monster\"");
                sw.WriteLine("GuideMonster.MainMenuIndex = 2");
                sw.WriteLine("GuideMonster.MainMenuTitle = \"Monster\"");

                foreach (var mob in mobs)
                {            
                    if(!(spawnData.ContainsKey(mob.Id)))
                    {
                        continue;
                    }

                    string aegisName = mob.AegisName;
                    
                    // Calcula level range (padrão: min = max(1, lvl-10); max = min + 15)
                    int minLv = Math.Max(1, mob.Level - 15);
                    int maxLv = mob.Level + 15;

                    // Monta string de drops: ^i[id] ^i[id2] ...
                    var dropIds = new List<int>();
                    foreach (var d in mob.Drops)
                    {
                        if (d == null) continue;
                        if (itemdb.TryGetValue(d.AegisName, out var itm))
                        {
                            if (!dropIds.Contains(itm.Id))
                                dropIds.Add(itm.Id);
                        }
                    }

                    // Se não há drops mapeados, podemos pular ou gerar com string vazia
                    if (dropIds.Count == 0)
                    {
                        if(Properties.Settings.Default.dropable)
                            continue;
                    }

                    string dropsStr = string.Join(" ", dropIds.Select(i => $"^i[{i}]"));

                    // Escreve a linha GuideMonster:AddContents("AegisName", min, max, "drops")
                    // Observação: no seu modelo o nome do monstro é string simples (sem escapar), garante que o aegisName esteja limpo
                    sw.WriteLine($"GuideMonster:AddContents(\"{aegisName}\", {minLv}, {maxLv}, \"{dropsStr}\")");
                }
            }
        }

        private void GenerateGuideWeaponLua(Dictionary<int, TipBoxInfo> TipBoxes, Dictionary<int, Monster> mobdb, Dictionary<string, Items> itemdb)
        {
            // Ordena monstros por level asc (se empate, por Id)
            var items = itemdb.Values
                .OrderBy(m => m.EquipLevelMin)
                .ThenBy(m => m.Id)
                .ToList();

            using (var sw = new StreamWriter("guide_weapon.lub", false, tipansi.IsChecked == true ? Encoding.GetEncoding(1252) : Encoding.UTF8))
            {
                sw.WriteLine("GuideWeapon.TitleText = \"Weapon\"");
                sw.WriteLine("GuideWeapon.MainMenuIndex = 5");
                sw.WriteLine("GuideWeapon.MainMenuTitle = \"Weapon\"");
                sw.WriteLine("GuideWeapon:AddSubMenu(0, \"Dagger\", 1, 999)");
                sw.WriteLine("GuideWeapon:AddSubMenu(1, \"One - Handed Sword\", 1, 999)");              
                sw.WriteLine("GuideWeapon:AddSubMenu(2, \"One - Handed Spear\", 1, 999)");           
                sw.WriteLine("GuideWeapon:AddSubMenu(3, \"One - Handed Axe\", 1, 999)");
                sw.WriteLine("GuideWeapon:AddSubMenu(4, \"Mace\", 1, 999)");
                sw.WriteLine("GuideWeapon:AddSubMenu(5, \"One - Handed Staff\", 1, 999)");
                sw.WriteLine("GuideWeapon:AddSubMenu(6, \"Knuckle\", 1, 999)");
                sw.WriteLine("GuideWeapon:AddSubMenu(7, \"Musical Instrument\", 1, 999)");
                sw.WriteLine("GuideWeapon:AddSubMenu(8, \"Whip\", 1, 999)");
                sw.WriteLine("GuideWeapon:AddSubMenu(9, \"Book\", 1, 999)");
                sw.WriteLine("GuideWeapon:AddSubMenu(10, \"Two - Handed Sword\", 1, 999)");
                sw.WriteLine("GuideWeapon:AddSubMenu(11, \"Two - Handed Spear\", 1, 999)");
                sw.WriteLine("GuideWeapon:AddSubMenu(12, \"Two - Handed Axe\", 1, 999)");
                sw.WriteLine("GuideWeapon:AddSubMenu(13, \"Katar\", 1, 999)");
                sw.WriteLine("GuideWeapon:AddSubMenu(14, \"Bow\", 1, 999)");
                sw.WriteLine("GuideWeapon:AddSubMenu(15, \"Huuma Shuriken\", 1, 999)");
                sw.WriteLine("GuideWeapon:AddSubMenu(16, \"Two - Handed Staff\", 1, 999)");
                sw.WriteLine("GuideWeapon:AddSubMenu(17, \"Revover\", 1, 999)");
                sw.WriteLine("GuideWeapon:AddSubMenu(18, \"Rifle\", 1, 999)");
                sw.WriteLine("GuideWeapon:AddSubMenu(19, \"Gatling Gun\", 1, 999)");
                sw.WriteLine("GuideWeapon:AddSubMenu(20, \"Shotgun\", 1, 999)");                        
                sw.WriteLine("GuideWeapon:AddSubMenu(21, \"Grenade Launcher\", 1, 999)");
                foreach (var item in items)
                {
                    if (item.Type.ToLower() != "weapon")
                        continue;

                    int minLv = Math.Max(item.EquipLevelMin - 100, 1);
                    int maxLv = Math.Min(999, minLv + 100);
                    int tipboxId = GetTipBoxPosition(TipBoxes, item.Id);
                    if (Properties.Settings.Default.dropable)
                    {
                        if (tipboxId <= 0)
                            continue;
                    }
                    string stream = $"GuideWeapon:AddContents({item.Id}, {GetWeaponTableValue(item.SubType)}, {minLv}, {maxLv}, {item.Refinable}, 0, {item.Gradeable}, \"\",";
                    if (tipboxId >= 0)
                        stream += $"\">> <TIPBOX>{item.Name}<INFO>{tipboxId}</INFO></TIPBOX>\")";
                    else
                        stream += "\"\")";
                    sw.WriteLine(stream);
                }
            }
        }

        private int GetTipBoxPosition(Dictionary<int, TipBoxInfo> TipBoxes, int id)
        {
            var sortedKeys = TipBoxes.Keys.OrderBy(k => k).ToList();
            foreach (var it in sortedKeys)
            {
                if (TipBoxes[it].Type != dbTypes.ITEM)
                    continue;
                if (TipBoxes[it].Id == id)
                    return it;
            }
            return -1;
        }

        private int GetWeaponTableValue(string val)
        {
            switch (val.ToLower())
            {
                case "dagger": return 0;
                case "1hspear": return 2;
                case "1hsword": return 1;
                case "1haxe": return 3;
                case "mace": return 4;
                case "staff": return 5;
                case "knuckle": return 6;
                case "musical": return 7;
                case "whip": return 8;
                case "book": return 9;
                case "2hsword": return 10;
                case "2hspear": return 11;
                case "2haxe": return 12;
                case "katar": return 13;
                case "bow": return 14;
                case "huuma": return 15;
                case "2hstaff": return 16;
                case "revolver": return 17;
                case "rifle": return 18;
                case "gatling": return 19;
                case "shotgun": return 20;
                case "grenade": return 21;
            }
            return 0;
        }

        Dictionary<string, int> locationMap = new Dictionary<string, int>
        {
            { "Head_Top", 0 },
            { "Head_Mid", 1 },
            { "Head_Low", 2 },
            { "Armor", 3 },
            { "Garment", 4 },
            { "Left_Hand", 5 },    // Shield
            { "Shoes", 6 },
            { "Accessory", 7 },
            { "Right_Accessory", 8 },
            { "Left_Accessory", 9 },
            { "Both_Accessory", 7 } // Pode ajustar depois
        };

        private void GenerateGuideArmorLua(Dictionary<int, TipBoxInfo> TipBoxes, Dictionary<int, Monster> mobdb, Dictionary<string, Items> itemdb)
        {
            // Ordena monstros por level asc (se empate, por Id)
            var items = itemdb.Values
                .OrderBy(m => m.EquipLevelMin)
                .ThenBy(m => m.Id)
                .ToList();

            using (var sw = new StreamWriter("guide_armor.lub", false, tipansi.IsChecked == true ? Encoding.GetEncoding(1252) : Encoding.UTF8))
            {
                sw.WriteLine("GuideArmor.TitleText = \"Armor\"");
                sw.WriteLine("GuideArmor.MainMenuIndex = 6");
                sw.WriteLine("GuideArmor.MainMenuTitle = \"Armor\"");
                sw.WriteLine("GuideArmor:AddSubMenu(0, \"Upper Headgear\", 1, 999)");
                sw.WriteLine("GuideArmor:AddSubMenu(1, \"Middle Headgear\", 1, 999)");
                sw.WriteLine("GuideArmor:AddSubMenu(2, \"Lower Headgear\", 1, 999)");
                sw.WriteLine("GuideArmor:AddSubMenu(3, \"Armor\", 1, 999)");
                sw.WriteLine("GuideArmor:AddSubMenu(4, \"Garment\", 1, 999)");
                sw.WriteLine("GuideArmor:AddSubMenu(5, \"Shield\", 1, 999)");
                sw.WriteLine("GuideArmor:AddSubMenu(6, \"Shoes\", 1, 999)");
                sw.WriteLine("GuideArmor:AddSubMenu(7, \"Accessory\", 1, 999)");
                sw.WriteLine("GuideArmor:AddSubMenu(8, \"Accessory Right\", 1, 999)");
                sw.WriteLine("GuideArmor:AddSubMenu(9, \"Accessory Left\", 1, 999)");
                foreach (var item in items)
                {
                    if (item.Type.ToLower() != "armor")
                        continue;
                    
                    int minLv = Math.Max(item.EquipLevelMin,1);
                    int maxLv = Math.Min(275, minLv + 100);

                    int tipboxId = GetTipBoxPosition(TipBoxes, item.Id);
                    if (Properties.Settings.Default.dropable)
                    {
                        if (tipboxId <= 0)
                            continue;
                    }
                    string stream = $"GuideArmor:AddContents({item.Id}, {item.Location}, {minLv}, {maxLv}, {item.Refinable}, 0, {item.Gradeable}, \"\",";
                    if (tipboxId > 0)
                        stream += $"\">> <TIPBOX>{item.Name}<INFO>{tipboxId}</INFO></TIPBOX>\")";
                    else
                        stream += "\"\")";
                    sw.WriteLine(stream);
                }
            }
        }

    }
}
