using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotGES.Piramida.Report {
	public class VERReport:Report {
		public VERReport(DateTime dateStart, DateTime dateEnd, IntervalReportEnum interval) :
			base(dateStart, dateEnd, interval) {
			int pn=4;			
			ReportMBRecords.AddRecordsMBW(this, pn, 1, 1, true, false, DBOperEnum.avg, ResultTypeEnum.sum);
			
		}


		public override void ReadData() {
			base.ReadData();
		}

	}

	public class VERReportMonth : Report {
		public VERReportMonth(DateTime dateStart, DateTime dateEnd, IntervalReportEnum interval) :
			base(dateStart, dateEnd, interval) {
			int pn = 12;
			ReportMBRecords.AddRecordsMBW(this, pn, 1, 1, true, false, DBOperEnum.eq, ResultTypeEnum.sum);
		}


		public override void ReadData() {
			base.ReadData();
		}

	}

}
