using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using GalaSoft.MvvmLight.CommandWpf;
using net.sf.mpxj.mpp;
using net.sf.mpxj.mpx;
using net.sf.mpxj.mspdi;

namespace PlanConverter {
    public class MppToMspdiConverter {

        public MppToMspdiConverter() {
            AddFiles = new RelayCommand<IEnumerable<string>>(files => {
                foreach (var file in files) {
                    ConvertFiles.Add(new MspdiConvertFile(file));
                }
            });

            Convert = new RelayCommand(() => {
                if (ConvertFiles.Count == 0) {
                    return;
                }
                foreach (var file in ConvertFiles) {
                    file.Convert();
                }
            });
        }

        public ObservableCollection<MspdiConvertFile> ConvertFiles { get; } = new ObservableCollection<MspdiConvertFile>();
        public RelayCommand<IEnumerable<string>> AddFiles { get; private set; }
        public RelayCommand Convert { get; private set; }
    }

    public class MspdiConvertFile {
        private readonly string _mpp;
        private readonly string _xml;

        public MspdiConvertFile(string mpp) {
            _mpp = mpp;
            _xml = Path.GetDirectoryName(_mpp) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(_mpp) + ".mspdi";
        }


        public void Convert() {
            var reader = new MPPReader();
            var project = reader.read(_mpp);
            var writer = new MSPDIWriter();
            writer.write(project, _xml);
        }

        public override string ToString() {
            return _xml;
        }
    }
}