using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using java.io;
using java.nio.charset;
using java.util;
using net.sf.mpxj;
using net.sf.mpxj.mpp;
using net.sf.mpxj.mpx;

namespace PlanConverter {
    public class MppToMpxConverter : ViewModelBase {
        public MppToMpxConverter() {
            AddFiles = new RelayCommand<IEnumerable<string>>(files => {
                foreach (var file in files) {
                    ConvertFiles.Add(new MpxConvertFile(file));
                }
            });

            Convert = new RelayCommand(() => {
                if (ConvertFiles.Count == 0) {
                    return;
                }
                //Task.WaitAll(ConvertFiles.Select(_ => _.ConvertTask).ToArray());
                foreach (var file in ConvertFiles) {
                    file.Convert();
                }
            });
        }

        public ObservableCollection<MpxConvertFile> ConvertFiles { get; } = new ObservableCollection<MpxConvertFile>();
        public RelayCommand<IEnumerable<string>> AddFiles { get; private set; }
        public RelayCommand Convert { get; private set; }

    }

    public class MpxConvertFile {
        private readonly string _mpp;
        private readonly string _mpx;

        public MpxConvertFile(string mpp) {
            _mpp = mpp;
            _mpx = Path.GetDirectoryName(_mpp) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(_mpp) + ".mpx";
        }


        public void Convert()  {
            var reader = new MPPReader();
            ProjectFile project = reader.read(_mpp);
            project.ProjectProperties.MpxCodePage = CodePage.RU;

            var writer = new MPXWriter();
            writer.write(project, new FileOutputStream(_mpx));
        }
    
        public override string ToString() {
            return _mpx;
        }
    }
}