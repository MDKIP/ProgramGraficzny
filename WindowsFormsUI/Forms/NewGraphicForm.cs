using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using ProgramGraficznyClasses;

namespace WindowsFormsUI
{
    public partial class NewGraphicForm : Form
    {
        public NewGraphicForm(IProject project, ILog log, INotificator notificator)
        {
            // Wywalanie wyjątków.
            if (log == null && notificator == null)
            {
                throw new NullReferenceException("log i notificator są puste");
            }
            else if (log == null)
            {
                Exception error = new NullReferenceException(Factory.GetProgrammerErrorString("log nie może być pusty.", true));
                notificator.Notify(error);
                throw error;
            }
            else if (notificator == null)
            {
                Exception error = new NullReferenceException(Factory.GetProgrammerErrorString("notificator nie może być pusty.", true));
                log.Write(error.Message);
                throw error;
            }

            log.Write(Factory.GetNewInstanceCreationString("NewGraphicForm"), LogMessagesTypes.Detail);

            // Przypisywanie.
            this.notificator = notificator;
            this.project = project;
            this.log = log;

            // Inicjalizacja komponentów.
            InitializeComponent();
        }

        private Image imgForImgTypeProject;
        private int sizeOfPixelArt = 32;
        private INotificator notificator;
        private IProject project;
        private ILog log;
        private GraphicTypes type;

        /// <summary>
        /// Ustawia layout do typu.
        /// </summary>
        /// <param name="t">Typ.</param>
        private void SetLayout(GraphicTypes t)
        {
            switch (t)
            {
                case GraphicTypes.Empty:
                    lblWidth.Visible = true;
                    lblHeight.Visible = true;
                    lblPathToImage.Visible = false;
                    lblWidthOfSelectedImage.Visible = false;
                    lblHeightOfSelectedImage.Visible = false;
                    lblSizeOfPixelArt.Visible = false;
                    lblSizePerPixel.Visible = false;
                    txtWidth.Visible = true;
                    txtHeight.Visible = true;
                    btnSelectImage.Visible = false;
                    cmbSizeOfPixelArt.Visible = false;
                    nudSizePerPixel.Visible = false;
                    log.Write("NewGraphicForm layout jest od teraz Empty.", LogMessagesTypes.Important);
                    break;
                case GraphicTypes.Image:
                    lblWidth.Visible = false;
                    lblHeight.Visible = false;
                    lblPathToImage.Visible = true;
                    lblWidthOfSelectedImage.Visible = true;
                    lblHeightOfSelectedImage.Visible = true;
                    lblSizeOfPixelArt.Visible = false;
                    lblSizePerPixel.Visible = false;
                    txtWidth.Visible = false;
                    txtHeight.Visible = false;
                    btnSelectImage.Visible = true;
                    cmbSizeOfPixelArt.Visible = false;
                    nudSizePerPixel.Visible = false;
                    log.Write("NewGraphicForm layout jest od teraz Image.", LogMessagesTypes.Important);
                    break;
                case GraphicTypes.PixelArt:
                    lblWidth.Visible = false;
                    lblHeight.Visible = false;
                    lblPathToImage.Visible = false;
                    lblWidthOfSelectedImage.Visible = false;
                    lblHeightOfSelectedImage.Visible = false;
                    lblSizeOfPixelArt.Visible = true;
                    lblSizePerPixel.Visible = true;
                    txtWidth.Visible = false;
                    txtHeight.Visible = false;
                    btnSelectImage.Visible = false;
                    cmbSizeOfPixelArt.Visible = true;
                    nudSizePerPixel.Visible = true;
                    log.Write("NewGraphicForm layout jest od teraz PixelArt.", LogMessagesTypes.Important);
                    break;
            }
        }

        private void NewGraphicForm_Load(object sender, EventArgs e)
        {
            log.Write("NewGraphicForm został załadowany.", LogMessagesTypes.Important);

            Text = "Nowa Grafika";

            SetLayout(GraphicTypes.Empty);
        }
        private void btnCreate_Click(object sender, EventArgs e)
        {
            log.Write("Tworznie nowej grafiki.", LogMessagesTypes.Important);
            switch (type)
            {
                case GraphicTypes.Empty:
                    {
                        log.Write("Tworzenie nowej grafki o typie Empty.", LogMessagesTypes.Important);
                        int height1 = 0;
                        int width1 = 0;
                        if (!int.TryParse(txtWidth.Text, out width1) || !int.TryParse(txtHeight.Text, out height1))
                        {
                            notificator.Notify((new Exception("Wymagane jest podanie liczb w polu na szerkość i wysokość.").Message));
                            log.Write("USER ERROR: Użytkownik nie podał w szerokości i wyskokości liczb tylko tekst.", LogMessagesTypes.Important);
                            return;
                        }
                        if (width1 == 21 && height1 == 37)
                        {
                            notificator.Notify("no i ja się pytam człowieku dumny ty jesteś z siebie zdajesz sobie sprawę z tego co robisz?masz ty wogóle rozum i godnośc człowieka?ja nie wiem ale żałosny typek z ciebie ,chyba nie pomyślałes nawet co robisz i kogo obrażasz ,możesz sobie obrażac tych co na to zasłużyli sobie ale nie naszego papieża polaka naszego rodaka wielką osobę ,i tak wyjątkowa i ważną bo to nie jest ktoś tam taki sobie że możesz go sobie wyśmiać bo tak ci się podoba nie wiem w jakiej ty się wychowałes rodzinie ale chyba ty nie wiem nie rozumiesz co to jest wiara .jeśli myslisz że jestes wspaniały to jestes zwykłym czubkiem którego ktoś nie odizolował jeszcze od społeczeństwa ,nie wiem co w tym jest takie śmieszne ale czepcie się stalina albo hitlera albo innych zwyrodnialców a nie czepiacie się takiej świętej osoby jak papież jan paweł 2 .jak można wogóle publicznie zamieszczac takie zdięcia na forach internetowych?ja się pytam kto powinien za to odpowiedziec bo chyba widac że do koscioła nie chodzi jak jestes nie wiem ateistą albo wierzysz w jakies sekty czy wogóle jestes może ty sługą szatana a nie będziesz z papieża robił takiego ,to ty chyba jestes jakis nie wiem co sie jarasz pomiotami szatana .wez pomyśl sobie ile papież zrobił ,on był kimś a ty kim jestes żeby z niego sobie robić kpiny co? kto dał ci prawo obrażac wogóle papieża naszego ?pomyślałes wogóle nad tym że to nie jest osoba taka sobie że ją wyśmieje i mnie będa wszyscy chwalic? wez dziecko naprawdę jestes jakis psycholek bo w przeciwieństwie do ciebie to papież jest autorytetem dla mnie a ty to nie wiem czyim możesz być autorytetem chyba takich samych jakiś głupków jak ty którzy nie wiedza co to kosciół i religia ,widac że się nie modlisz i nie chodzisz na religie do szkoły ,widac nie szanujesz religii to nie wiem jak chcesz to sobie wez swoje zdięcie wstaw ciekawe czy byś sie odważył .naprawdę wezta się dzieci zastanówcie co wy roicie bo nie macie widac pojęcia o tym kim był papież jan paweł2 jak nie jestescie w pełni rozwinięte umysłowo to się nie zabierajcie za taką osobę jak ojciec swięty bo to świadczy o tym że nie macie chyba w domu krzyża ani jednego obraza świętego nie chodzi tutaj o kosciół mnie ale wogóle ogólnie o zasady wiary żeby mieć jakąs godnosc bo papież nikogo nie obrażał a ty za co go obrażasz co? no powiedz za co obrażasz taką osobę jak ojciec święty ?brak mnie słów ale jakbyś miał pojęcie chociaz i sięgnął po pismo święte i poczytał sobie to może byś się odmienił .nie wiem idz do kościoła bo widac już dawno szatan jest w tobie człowieku ,nie lubisz kościoła to chociaż siedz cicho i nie obrażaj innych ludzi");
                            log.Write("Pasta została wyświetlona xd.", LogMessagesTypes.Important);
                        }
                        GraphicsEditor editor = new GraphicsEditor(project, ProgramInfo.MainToolbox, GraphicTypes.Empty, log, notificator, null);
                        editor.SetWorkSpaceSize(new Size(width1, height1));
                        editor.Show();
                        //notificator.Notify("Wybacz ale ten tryb grafiki jest niedostępny w tej wersji programu.");
                    }
                    break;
                case GraphicTypes.Image:
                    {
                        log.Write("Tworzenie nowej grafki o typie Image.", LogMessagesTypes.Important);
                        if (imgForImgTypeProject == null)
                        {
                            notificator.Notify(new Exception("Wymagane jest wybranie zdjęcia.").Message);
                            log.Write("USER ERROR: Użytkownik nie wybrał zdjęcia.", LogMessagesTypes.Important);
                            return;
                        }
                        GraphicsEditor editor = new GraphicsEditor(project, ProgramInfo.MainToolbox, GraphicTypes.Image, log, notificator, null);
                        editor.SetWorkSpaceSize(imgForImgTypeProject.Size);
                        editor.Show();
                        editor.Graphic.DrawImage(imgForImgTypeProject, 0, 0);
                        //notificator.Notify("Wybacz ale ten tryb grafiki jest niedostępny w tej wersji programu.");
                    }
                    break;
                case GraphicTypes.PixelArt:
                    {
                        if (ProgramInfo.MainPixelArtToolbox == null)
                        {
                            PixelArtToolboxForm toolbox = new PixelArtToolboxForm(log, notificator);
                            ProgramInfo.MainPixelArtToolbox = toolbox;
                            toolbox.Show();
                        }
                        PixelArtEditor editor = new PixelArtEditor(sizeOfPixelArt, ProgramInfo.MainPixelArtToolbox, project, log, notificator);
                        editor.RealPixelsPerEditorPixels = (int)nudSizePerPixel.Value;
                        editor.Show();
                    }
                    break;
            }
            Close();
        }
        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            log.Write("Użytkownik chce wybrać zdjęcie do nowej grafiki.", LogMessagesTypes.Important);
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (!dialog.FileName.EndsWith(".jpg"))
                {
                    notificator.Notify((new Exception("Niestety chwilowo Program Graficzny obsługuje tylko pliki JPG. Prosimy wybrać plik JPG").Message));
                    return;
                }
                imgForImgTypeProject = Image.FromFile(dialog.FileName);
                lblPathToImage.Text = $"Ścieżka: {dialog.FileName}";
                lblWidthOfSelectedImage.Text = $"Szerokość: {imgForImgTypeProject.Width}";
                lblHeightOfSelectedImage.Text = $"Wysokość: {imgForImgTypeProject.Height}";
            }
        }
        private void cmbGraphicType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbGraphicType.Text)
            {
                case "Pusta":
                    type = GraphicTypes.Empty;
                    break;
                case "Zdjęcie":
                    type = GraphicTypes.Image;
                    break;
                case "Piksel Art":
                    type = GraphicTypes.PixelArt;
                    break;
                default:
                    notificator.Notify((new Exception(Factory.GetProgrammerErrorString("Nie wykryto typu nowej grafiki.", false))).Message);
                    break;
            }
            SetLayout(type);
        }
        private void cmbSizeOfPixelArt_SelectedIndexChanged(object sender, EventArgs e)
        {
            sizeOfPixelArt = int.Parse(cmbSizeOfPixelArt.Text);
        }
    }
}
