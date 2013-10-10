using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PIE.API;
using PIE.Models;

namespace PIE.Controls
{
    public partial class DlgDataBand : Form
    {
        private DataVar _Model;
        private int _BandTotal;
        private int _MaxXSize;
        private int _MaxYSize;
        private List<int> _SpectralSubset = null;
        private int[] _SpatialSubset = null;

        public DataVar Model { get { return _Model; } }

        public bool AsBand
        {
            get { return !ChbxAsSpectral.Checked; }
            protected set 
            {
                ChbxAsSpectral.Checked = value;
                BtViewSpectral.Enabled = value;
            }
        }

        //TODO: check
        public string File
        {
            get { return TbxFile.Text; }
            protected set 
            {
                TbxFile.Text = value;
                if (string.IsNullOrWhiteSpace(value))
                {
                    BtSelectSpectralSubset.Enabled = false;
                    BtSelectSpatialSubset.Enabled = false;
                    BtSetHeader.Enabled = false;
                }
                else
                {
                    GDAL.AllRegister();
                    IntPtr hdataset = GDAL.Open(TbxFile.Text, GDAL.GA_ReadOnly);
                    _BandTotal = GDAL.GetRasterCount(hdataset);
                    _MaxXSize = GDAL.GetRasterXSize(hdataset);
                    _MaxYSize = GDAL.GetRasterYSize(hdataset);
                    GDAL.Close(hdataset);
                    if (_BandTotal == 0 || _MaxXSize == 0 || _MaxYSize == 0)
                    {
                        LblSpec.Text = "";
                        LblSpat.Text = "";
                        BtSetHeader.Enabled = true;
                        _SpectralSubset.Clear();
                        _SpectralSubset = null;
                        _SpatialSubset = null;
                    }
                    else
                    {
                        if (_SpectralSubset == null)
                        {
                            _SpectralSubset = new List<int>();
                            for (int i = 0; i < _BandTotal; i++)
                                _SpectralSubset.Add(i + 1);
                            LblSpec.Text = "已选择波段：" + string.Join(",", _SpectralSubset);
                        }
                        if (_SpatialSubset == null)
                        {
                            _SpatialSubset = new int[] { 1, _MaxXSize, 1, _MaxYSize };
                            LblSpat.Text = string.Format("已选择空间：X[{0},{1}],Y[{2},{3}]",
                                _SpatialSubset[0],
                                _SpatialSubset[1],
                                _SpatialSubset[2],
                                _SpatialSubset[3]
                            );
                        }
                        BtSelectSpectralSubset.Enabled = true;
                        BtSelectSpatialSubset.Enabled = true;
                        BtSetHeader.Enabled = false;
                    }
                }
            }
        }

        public DlgDataBand(DataVar model)
        {
            InitializeComponent();
            _Model = model;
        }

        private void FrmMappingDataVar_Load(object sender, EventArgs e)
        {
            _SpectralSubset = _Model.SpectralSubset;
            _SpatialSubset = _Model.SpatialSubset;
            File = _Model.File;
            AsBand = !_Model.AsBand;
        }

        private void BtBrowse_Click(object sender, EventArgs e)
        {
            if (dlgfile.ShowDialog() != DialogResult.OK) return;
            File = dlgfile.FileName;
        }

        private void BtOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TbxFile.Text))
            {
                MessageBox.Show("请选择数据文件。", "数据验证");
                return;
            }
            if (_SpectralSubset == null)
            {
                MessageBox.Show("请选择光谱子集。", "数据验证");
                return;
            }
            if (_SpatialSubset == null)
            {
                MessageBox.Show("请选择空间子集。", "数据验证");
                return;
            }
            _Model.File = TbxFile.Text;
            _Model.SpectralSubset = _SpectralSubset;
            _Model.SpatialSubset = _SpatialSubset;
            DialogResult = DialogResult.OK;
        }

        private void BtCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void BtSelectSpectralSubset_Click(object sender, EventArgs e)
        {
            //TODO: pass SpecSubset to dlgspec
            DlgSpectralSubset dlg = new DlgSpectralSubset(_BandTotal,_BandTotal);
            if (dlg.ShowDialog() != DialogResult.OK) return;
            _SpectralSubset = dlg.Subset;
            LblSpec.Text = "已选择波段：" + string.Join(",", _SpectralSubset);
        }

        private void BtSelectSpatialSubset_Click(object sender, EventArgs e)
        {
            //TODO: pass SpatSubset to dlgspat
            DlgSpatialSubset dlg = new DlgSpatialSubset(_MaxXSize, _MaxYSize);
            if (dlg.ShowDialog() != DialogResult.OK) return;
            _SpatialSubset = dlg.Subset;
            LblSpat.Text = string.Format("已选择空间：X[{0},{1}],Y[{2},{3}]",
                _SpatialSubset[0],
                _SpatialSubset[1],
                _SpatialSubset[2],
                _SpatialSubset[3]
            );
        }

        private void ChbxAsSpectral_CheckedChanged(object sender, EventArgs e)
        {
            BtViewSpectral.Enabled = ChbxAsSpectral.Checked;
        }
    }
}
