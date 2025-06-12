using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace LousaInterativa
{
    public class DrawingSurfaceForm : Form
    {
        private List<DrawableLine> _drawnLines = new List<DrawableLine>();
        private FrmLousaInterativa _ownerForm; // Referência ao formulário principal

        // Propriedades para cor e espessura da linha atual
        // Estas serão definidas pela FrmLousaInterativa
        public Color CurrentLineColor { get; set; } = Color.Black;
        public int CurrentLineWidth { get; set; } = 1;
        public Point? CurrentLineStartPoint { get; set; } = null; // Para a ferramenta Linhas

        // Propriedades para a ferramenta Borracha
        public bool IsEraserActive { get; set; } = false;
        public int EraserSize { get; set; } = 10; // Default size, can be updated by FrmLousaInterativa
        private List<Rectangle> _eraserMarks = new List<Rectangle>();

        // Propriedade para saber se a ferramenta Linhas está ativa (para o desenho do marcador)
        // Esta propriedade deve ser atualizada por FrmLousaInterativa
        public bool IsLinesToolActive { get; set; } = false;
        public DrawableLine SelectedLine { get; set; } = null;


        public DrawingSurfaceForm(FrmLousaInterativa owner)
        {
            _ownerForm = owner;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Name = "DrawingSurfaceForm";
            this.Text = "Drawing Surface"; // Não visível
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.Manual;
            // Usar uma cor que provavelmente não será usada pelo usuário para a chave de transparência.
            // Magenta é uma escolha comum.
            this.BackColor = Color.FromArgb(255, 1, 2, 3); // Uma cor bem específica e opaca
            this.TransparencyKey = this.BackColor;
            this.TopMost = true; // Para garantir que fique sobre a maioria das janelas, mas abaixo de FrmLousaInterativa se FrmLousaInterativa também for TopMost. Ajustar se necessário.

            // Habilitar double buffering para desenho mais suave
            this.DoubleBuffered = true;

            this.Paint += DrawingSurfaceForm_Paint;
            // this.MouseClick += DrawingSurfaceForm_MouseClick; // MouseClick is now less relevant as FrmLousaInterativa handles MouseDown/Move/Up
                                                              // However, FrmLousaInterativa.HandleSurfaceMouseClick still exists and filters by _isEraserToolActive
                                                              // So, keeping this registered is fine for now.
            this.MouseClick += DrawingSurfaceForm_MouseClick;
        }

        private void DrawingSurfaceForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            foreach (DrawableLine line in _drawnLines)
            {
                if (line == null) continue;
                using (Pen linePen = new Pen(line.LineColor, line.LineWidth))
                {
                    linePen.StartCap = LineCap.Round;
                    linePen.EndCap = LineCap.Round;
                    e.Graphics.DrawLine(linePen, line.StartPoint, line.EndPoint);
                }
            }

            // Desenhar marcador de ponto inicial para a ferramenta Linhas (se estiver nesta superfície)
            // Usar a propriedade IsLinesToolActive desta classe, que é definida por FrmLousaInterativa
            if (this.IsLinesToolActive && this.CurrentLineStartPoint != null)
            {
                 using (SolidBrush startPointBrush = new SolidBrush(Color.FromArgb(128, this.CurrentLineColor)))
                {
                    int markerSize = Math.Max(2, this.CurrentLineWidth / 2 + 2);
                    e.Graphics.FillEllipse(startPointBrush,
                                           this.CurrentLineStartPoint.Value.X - markerSize / 2,
                                           this.CurrentLineStartPoint.Value.Y - markerSize / 2,
                                           markerSize, markerSize);
                }
            }

            // Desenhar as marcas de borracha
            // Usar a TransparencyKey para "apagar"
            // Importante: Este FillRectangle deve usar as coordenadas do cliente da DrawingSurfaceForm.
            // A cor do pincel DEVE ser this.TransparencyKey para que funcione.
            using (SolidBrush eraserBrush = new SolidBrush(this.TransparencyKey))
            {
                foreach (Rectangle mark in _eraserMarks)
                {
                    e.Graphics.FillRectangle(eraserBrush, mark);
                }
            }

            // Desenhar feedback de seleção
            if (this.SelectedLine != null)
            {
                float minX = Math.Min(this.SelectedLine.StartPoint.X, this.SelectedLine.EndPoint.X);
                float minY = Math.Min(this.SelectedLine.StartPoint.Y, this.SelectedLine.EndPoint.Y);
                float maxX = Math.Max(this.SelectedLine.StartPoint.X, this.SelectedLine.EndPoint.X);
                float maxY = Math.Max(this.SelectedLine.StartPoint.Y, this.SelectedLine.EndPoint.Y);
                float padding = Math.Max(4, this.SelectedLine.LineWidth / 2.0f + 2);
                RectangleF bounds = new RectangleF(
                    minX - padding,
                    minY - padding,
                    (maxX - minX) + (2 * padding),
                    (maxY - minY) + (2 * padding)
                );

                using (System.Drawing.SolidBrush selectionBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(80, 0, 100, 255))) // Semi-transparente azul
                {
                    e.Graphics.FillRectangle(selectionBrush, bounds);
                }
            }
        }

        // Encaminha o clique do mouse para o formulário principal processar a lógica da ferramenta
        private void DrawingSurfaceForm_MouseClick(object sender, MouseEventArgs e)
        {
            // Converte as coordenadas do clique para as coordenadas da tela,
            // depois para as coordenadas do cliente de FrmLousaInterativa.
            Point screenPoint = this.PointToScreen(e.Location);
            Point ownerFormClientPoint = _ownerForm.PointToClient(screenPoint);

            // Cria novos MouseEventArgs com as coordenadas ajustadas
            MouseEventArgs newArgs = new MouseEventArgs(e.Button, e.Clicks, ownerFormClientPoint.X, ownerFormClientPoint.Y, e.Delta);

            // Chama o manipulador de clique do mouse do formulário principal
            // Isso requer que FrmLousaInterativa exponha um método público para isso, ou que estejamos usando eventos.
            // Por simplicidade, vamos assumir que _ownerForm tem um método público para lidar com isso.
             _ownerForm.HandleSurfaceMouseClick(newArgs); // Método a ser criado em FrmLousaInterativa
        }


        // Método para adicionar uma linha (chamado por FrmLousaInterativa)
        public void AddLine(DrawableLine line)
        {
            _drawnLines.Add(line);
            this.Invalidate(); // Redesenha a superfície
        }

        // Método para limpar todas as linhas (chamado por FrmLousaInterativa)
        public void ClearLines()
        {
            _drawnLines.Clear();
            ClearEraserMarks(); // Também limpa as marcas de borracha
            this.Invalidate();
        }

        public void ClearEraserMarks()
        {
            _eraserMarks.Clear();
            // Invalidate pode ser chamado pelo chamador (ex: ClearLines) ou aqui se usado separadamente
            // this.Invalidate();
        }

        public void AddEraserMark(Point location)
        {
            Rectangle eraserRect = new Rectangle(
                location.X - EraserSize / 2,
                location.Y - EraserSize / 2,
                EraserSize,
                EraserSize);
            _eraserMarks.Add(eraserRect);
            this.Invalidate();
        }

        public List<DrawableLine> GetLines()
        {
            return _drawnLines;
        }

        public void SetLines(List<DrawableLine> lines)
        {
            _drawnLines = lines ?? new List<DrawableLine>(); // Ensure _drawnLines is not null
            this.Invalidate();
        }

        public bool RemoveLine(DrawableLine lineToRemove)
        {
            bool removed = _drawnLines.Remove(lineToRemove);
            if (removed)
            {
                if (this.SelectedLine == lineToRemove)
                {
                    this.SelectedLine = null; // Deselect if the removed line was selected
                }
                this.Invalidate(); // Solicita redesenho para remover a linha visualmente
            }
            return removed;
        }
    }
}
