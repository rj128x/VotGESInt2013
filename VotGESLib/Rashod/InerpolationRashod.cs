﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotGES.Rashod
{
	public class InerpolationRashod
	{
		public static double[] getTabl(int ga) {
			double[] Tabl = new double[132];

			switch (ga) {
				case 1:
					Tabl[0] = 170.6;
					Tabl[1] = 145.0;
					Tabl[2] = 125.6;
					Tabl[3] = 112.1;
					Tabl[4] = 94.8;
					Tabl[5] = 84.1;
					Tabl[6] = 78.6;
					Tabl[7] = 79.4;
					Tabl[8] = 82.1;
					Tabl[9] = 81.2;
					Tabl[10] = 81.1;

					Tabl[11] = 272.3;
					Tabl[12] = 230.9;
					Tabl[13] = 199.4;
					Tabl[14] = 177.6;
					Tabl[15] = 156.9;
					Tabl[16] = 145.5;
					Tabl[17] = 137.5;
					Tabl[18] = 132.6;
					Tabl[19] = 130.8;
					Tabl[20] = 131.2;
					Tabl[21] = 130.6;

					Tabl[22] = 354.0;
					Tabl[23] = 307.5;
					Tabl[24] = 271.1;
					Tabl[25] = 245.1;
					Tabl[26] = 223.9;
					Tabl[27] = 208.4;
					Tabl[28] = 197.1;
					Tabl[29] = 189.2;
					Tabl[30] = 184.7;
					Tabl[31] = 183.2;
					Tabl[32] = 182.6;

					Tabl[33] = 427.2;
					Tabl[34] = 379.9;
					Tabl[35] = 340.8;
					Tabl[36] = 311.5;
					Tabl[37] = 287.5;
					Tabl[38] = 269.3;
					Tabl[39] = 255.3;
					Tabl[40] = 244.5;
					Tabl[41] = 237.9;
					Tabl[42] = 235.3;
					Tabl[43] = 233.5;

					Tabl[44] = 500.3;
					Tabl[45] = 451.2;
					Tabl[46] = 409.8;
					Tabl[47] = 378.0;
					Tabl[48] = 350.8;
					Tabl[49] = 329.6;
					Tabl[50] = 312.7;
					Tabl[51] = 299.8;
					Tabl[52] = 290.9;
					Tabl[53] = 286.3;
					Tabl[54] = 283.1;

					Tabl[55] = 578.7;
					Tabl[56] = 526.4;
					Tabl[57] = 481.7;
					Tabl[58] = 445.6;
					Tabl[59] = 415.1;
					Tabl[60] = 390.3;
					Tabl[61] = 370.3;
					Tabl[62] = 354.7;
					Tabl[63] = 343.8;
					Tabl[64] = 336.7;
					Tabl[65] = 331.9;

					Tabl[66] = 680.7;
					Tabl[67] = 616.3;
					Tabl[68] = 563.3;
					Tabl[69] = 518.9;
					Tabl[70] = 482.0;
					Tabl[71] = 452.5;
					Tabl[72] = 428.8;
					Tabl[73] = 409.9;
					Tabl[74] = 397.0;
					Tabl[75] = 387.5;
					Tabl[76] = 380.3;

					Tabl[77] = 820.3;
					Tabl[78] = 730.3;
					Tabl[79] = 661.1;
					Tabl[80] = 600.8;
					Tabl[81] = 554.1;
					Tabl[82] = 518.3;
					Tabl[83] = 490.1;
					Tabl[84] = 466.9;
					Tabl[85] = 451.3;
					Tabl[86] = 439.6;
					Tabl[87] = 429.0;

					Tabl[88] = 972.8;
					Tabl[89] = 855.4;
					Tabl[90] = 765.2;
					Tabl[91] = 691.6;
					Tabl[92] = 632.9;
					Tabl[93] = 589.3;
					Tabl[94] = 555.6;
					Tabl[95] = 527.8;
					Tabl[96] = 509.0;
					Tabl[97] = 494.0;
					Tabl[98] = 479.7;

					Tabl[99] = 972.8;
					Tabl[100] = 1002.3;
					Tabl[101] = 880.9;
					Tabl[102] = 792.8;
					Tabl[103] = 720.2;
					Tabl[104] = 666.1;
					Tabl[105] = 626.1;
					Tabl[106] = 593.7;
					Tabl[107] = 572.4;
					Tabl[108] = 553.8;
					Tabl[109] = 536.0;

					Tabl[110] = 972.8;
					Tabl[111] = 1002.3;
					Tabl[112] = 1008.1;
					Tabl[113] = 903.0;
					Tabl[114] = 820.1;
					Tabl[115] = 753.4;
					Tabl[116] = 704.9;
					Tabl[117] = 666.9;
					Tabl[118] = 642.0;
					Tabl[119] = 620.8;
					Tabl[120] = 601.0;

					Tabl[121] = 972.8;
					Tabl[122] = 1002.3;
					Tabl[123] = 1147.0;
					Tabl[124] = 1022.0;
					Tabl[125] = 932.0;
					Tabl[126] = 851.0;
					Tabl[127] = 784.0;
					Tabl[128] = 747.0;
					Tabl[129] = 717.0;
					Tabl[130] = 694.0;
					Tabl[131] = 674.0;

					break;
				case 2:
					Tabl[0] = 153.0;
					Tabl[1] = 139.0;
					Tabl[2] = 129.0;
					Tabl[3] = 120.0;
					Tabl[4] = 115.0;
					Tabl[5] = 111.0;
					Tabl[6] = 109.0;
					Tabl[7] = 108.0;
					Tabl[8] = 108.0;
					Tabl[9] = 109.0;
					Tabl[10] = 108.0;
					Tabl[11] = 226.0;
					Tabl[12] = 207.0;
					Tabl[13] = 192.0;
					Tabl[14] = 179.0;
					Tabl[15] = 169.0;
					Tabl[16] = 162.0;
					Tabl[17] = 156.0;
					Tabl[18] = 151.0;
					Tabl[19] = 147.0;
					Tabl[20] = 142.0;
					Tabl[21] = 138.0;
					Tabl[22] = 314.0;
					Tabl[23] = 288.0;
					Tabl[24] = 266.0;
					Tabl[25] = 247.0;
					Tabl[26] = 233.0;
					Tabl[27] = 220.0;
					Tabl[28] = 210.0;
					Tabl[29] = 199.0;
					Tabl[30] = 190.0;
					Tabl[31] = 183.0;
					Tabl[32] = 175.0;
					Tabl[33] = 408.0;
					Tabl[34] = 373.0;
					Tabl[35] = 342.0;
					Tabl[36] = 318.0;
					Tabl[37] = 297.0;
					Tabl[38] = 279.0;
					Tabl[39] = 264.0;
					Tabl[40] = 250.0;
					Tabl[41] = 238.0;
					Tabl[42] = 227.0;
					Tabl[43] = 217.0;
					Tabl[44] = 512.0;
					Tabl[45] = 462.0;
					Tabl[46] = 421.0;
					Tabl[47] = 389.0;
					Tabl[48] = 362.0;
					Tabl[49] = 339.0;
					Tabl[50] = 320.0;
					Tabl[51] = 303.0;
					Tabl[52] = 287.0;
					Tabl[53] = 272.0;
					Tabl[54] = 259.0;
					Tabl[55] = 624.0;
					Tabl[56] = 559.0;
					Tabl[57] = 506.0;
					Tabl[58] = 463.0;
					Tabl[59] = 428.0;
					Tabl[60] = 401.0;
					Tabl[61] = 377.0;
					Tabl[62] = 355.0;
					Tabl[63] = 336.0;
					Tabl[64] = 318.0;
					Tabl[65] = 302.0;
					Tabl[66] = 741.0;
					Tabl[67] = 661.0;
					Tabl[68] = 596.0;
					Tabl[69] = 542.0;
					Tabl[70] = 498.0;
					Tabl[71] = 463.0;
					Tabl[72] = 434.0;
					Tabl[73] = 409.0;
					Tabl[74] = 386.0;
					Tabl[75] = 366.0;
					Tabl[76] = 348.0;
					Tabl[77] = 872.0;
					Tabl[78] = 774.0;
					Tabl[79] = 692.0;
					Tabl[80] = 626.0;
					Tabl[81] = 573.0;
					Tabl[82] = 529.0;
					Tabl[83] = 493.0;
					Tabl[84] = 463.0;
					Tabl[85] = 437.0;
					Tabl[86] = 415.0;
					Tabl[87] = 396.0;
					Tabl[88] = 937.0;
					Tabl[89] = 862.0;
					Tabl[90] = 790.0;
					Tabl[91] = 721.0;
					Tabl[92] = 655.0;
					Tabl[93] = 590.0;
					Tabl[94] = 557.0;
					Tabl[95] = 521.0;
					Tabl[96] = 491.0;
					Tabl[97] = 466.0;
					Tabl[98] = 447.0;
					Tabl[99] = 1010.0;
					Tabl[100] = 955.0;
					Tabl[101] = 904.0;
					Tabl[102] = 835.0;
					Tabl[103] = 748.0;
					Tabl[104] = 678.0;
					Tabl[105] = 623.0;
					Tabl[106] = 583.0;
					Tabl[107] = 549.0;
					Tabl[108] = 522.0;
					Tabl[109] = 500.0;
					Tabl[110] = 1110.0;
					Tabl[111] = 1132.0;
					Tabl[112] = 1015.0;
					Tabl[113] = 921.0;
					Tabl[114] = 850.0;
					Tabl[115] = 766.0;
					Tabl[116] = 699.0;
					Tabl[117] = 649.0;
					Tabl[118] = 611.0;
					Tabl[119] = 581.0;
					Tabl[120] = 558.0;
					Tabl[121] = 1110.0;
					Tabl[122] = 1150.0;
					Tabl[123] = 1123.0;
					Tabl[124] = 979.0;
					Tabl[125] = 961.0;
					Tabl[126] = 854.0;
					Tabl[127] = 779.0;
					Tabl[128] = 719.0;
					Tabl[129] = 677.0;
					Tabl[130] = 643.0;
					Tabl[131] = 621.0;
					break;
				case 3:
					Tabl[0] = 294.0;
					Tabl[1] = 240.0;
					Tabl[2] = 195.0;
					Tabl[3] = 158.0;
					Tabl[4] = 127.0;
					Tabl[5] = 103.0;
					Tabl[6] = 89.0;
					Tabl[7] = 79.0;
					Tabl[8] = 76.0;
					Tabl[9] = 77.0;
					Tabl[10] = 81.0;
					Tabl[11] = 342.0;
					Tabl[12] = 292.0;
					Tabl[13] = 247.0;
					Tabl[14] = 211.0;
					Tabl[15] = 181.0;
					Tabl[16] = 156.0;
					Tabl[17] = 144.0;
					Tabl[18] = 133.0;
					Tabl[19] = 128.0;
					Tabl[20] = 124.0;
					Tabl[21] = 119.0;
					Tabl[22] = 393.0;
					Tabl[23] = 344.0;
					Tabl[24] = 299.0;
					Tabl[25] = 265.0;
					Tabl[26] = 236.0;
					Tabl[27] = 212.0;
					Tabl[28] = 199.0;
					Tabl[29] = 187.0;
					Tabl[30] = 179.0;
					Tabl[31] = 173.0;
					Tabl[32] = 166.0;
					Tabl[33] = 457.0;
					Tabl[34] = 399.0;
					Tabl[35] = 353.0;
					Tabl[36] = 310.0;
					Tabl[37] = 293.0;
					Tabl[38] = 270.0;
					Tabl[39] = 254.0;
					Tabl[40] = 240.0;
					Tabl[41] = 231.0;
					Tabl[42] = 223.0;
					Tabl[43] = 215.0;
					Tabl[44] = 536.0;
					Tabl[45] = 472.0;
					Tabl[46] = 420.0;
					Tabl[47] = 382.0;
					Tabl[48] = 351.0;
					Tabl[49] = 328.0;
					Tabl[50] = 300.0;
					Tabl[51] = 294.0;
					Tabl[52] = 282.0;
					Tabl[53] = 273.0;
					Tabl[54] = 263.0;
					Tabl[55] = 643.0;
					Tabl[56] = 560.0;
					Tabl[57] = 495.0;
					Tabl[58] = 449.0;
					Tabl[59] = 411.0;
					Tabl[60] = 386.0;
					Tabl[61] = 365.0;
					Tabl[62] = 348.0;
					Tabl[63] = 334.0;
					Tabl[64] = 322.0;
					Tabl[65] = 311.0;
					Tabl[66] = 755.0;
					Tabl[67] = 654.0;
					Tabl[68] = 575.0;
					Tabl[69] = 510.0;
					Tabl[70] = 475.0;
					Tabl[71] = 444.0;
					Tabl[72] = 420.0;
					Tabl[73] = 402.0;
					Tabl[74] = 385.0;
					Tabl[75] = 374.0;
					Tabl[76] = 361.0;
					Tabl[77] = 932.0;
					Tabl[78] = 786.0;
					Tabl[79] = 676.0;
					Tabl[80] = 602.0;
					Tabl[81] = 544.0;
					Tabl[82] = 505.0;
					Tabl[83] = 475.0;
					Tabl[84] = 456.0;
					Tabl[85] = 437.0;
					Tabl[86] = 426.0;
					Tabl[87] = 411.0;
					Tabl[88] = 954.0;
					Tabl[89] = 954.0;
					Tabl[90] = 800.0;
					Tabl[91] = 690.0;
					Tabl[92] = 624.0;
					Tabl[93] = 572.0;
					Tabl[94] = 533.0;
					Tabl[95] = 511.0;
					Tabl[96] = 491.0;
					Tabl[97] = 478.0;
					Tabl[98] = 464.0;
					Tabl[99] = 1015.0;
					Tabl[100] = 1015.0;
					Tabl[101] = 907.0;
					Tabl[102] = 810.0;
					Tabl[103] = 724.0;
					Tabl[104] = 649.0;
					Tabl[105] = 598.0;
					Tabl[106] = 571.0;
					Tabl[107] = 549.0;
					Tabl[108] = 533.0;
					Tabl[109] = 518.0;
					Tabl[110] = 1036.0;
					Tabl[111] = 1036.0;
					Tabl[112] = 1036.0;
					Tabl[113] = 919.0;
					Tabl[114] = 820.0;
					Tabl[115] = 739.0;
					Tabl[116] = 675.0;
					Tabl[117] = 638.0;
					Tabl[118] = 613.0;
					Tabl[119] = 592.0;
					Tabl[120] = 572.0;
					Tabl[121] = 1036.0;
					Tabl[122] = 1036.0;
					Tabl[123] = 1187.0;
					Tabl[124] = 1017.0;
					Tabl[125] = 912.0;
					Tabl[126] = 842.0;
					Tabl[127] = 764.0;
					Tabl[128] = 712.0;
					Tabl[129] = 683.0;
					Tabl[130] = 655.0;
					Tabl[131] = 626.0;
					break;
				case 4:
					Tabl[0] = 124;
					Tabl[1] = 119;
					Tabl[2] = 117;
					Tabl[3] = 110;
					Tabl[4] = 103;
					Tabl[5] = 97;
					Tabl[6] = 92;
					Tabl[7] = 88;
					Tabl[8] = 83;
					Tabl[9] = 80;
					Tabl[10] = 76;
					Tabl[11] = 202;
					Tabl[12] = 194;
					Tabl[13] = 191;
					Tabl[14] = 178;
					Tabl[15] = 166;
					Tabl[16] = 156;
					Tabl[17] = 147;
					Tabl[18] = 141;
					Tabl[19] = 133;
					Tabl[20] = 128;
					Tabl[21] = 122;
					Tabl[22] = 281;
					Tabl[23] = 269;
					Tabl[24] = 266;
					Tabl[25] = 246;
					Tabl[26] = 230;
					Tabl[27] = 215;
					Tabl[28] = 202;
					Tabl[29] = 194;
					Tabl[30] = 184;
					Tabl[31] = 176;
					Tabl[32] = 168;
					Tabl[33] = 360;
					Tabl[34] = 344;
					Tabl[35] = 341;
					Tabl[36] = 315;
					Tabl[37] = 294;
					Tabl[38] = 275;
					Tabl[39] = 258;
					Tabl[40] = 247;
					Tabl[41] = 235;
					Tabl[42] = 225;
					Tabl[43] = 214;
					Tabl[44] = 427;
					Tabl[45] = 408;
					Tabl[46] = 403;
					Tabl[47] = 374;
					Tabl[48] = 349;
					Tabl[49] = 326;
					Tabl[50] = 308;
					Tabl[51] = 291;
					Tabl[52] = 276;
					Tabl[53] = 264;
					Tabl[54] = 254;
					Tabl[55] = 505;
					Tabl[56] = 483;
					Tabl[57] = 477;
					Tabl[58] = 441;
					Tabl[59] = 412;
					Tabl[60] = 385;
					Tabl[61] = 363;
					Tabl[62] = 343;
					Tabl[63] = 324;
					Tabl[64] = 310;
					Tabl[65] = 297;
					Tabl[66] = 589;
					Tabl[67] = 563;
					Tabl[68] = 555;
					Tabl[69] = 513;
					Tabl[70] = 478;
					Tabl[71] = 448;
					Tabl[72] = 421;
					Tabl[73] = 396;
					Tabl[74] = 376;
					Tabl[75] = 358;
					Tabl[76] = 342;
					Tabl[77] = 670;
					Tabl[78] = 640;
					Tabl[79] = 624;
					Tabl[80] = 590;
					Tabl[81] = 548;
					Tabl[82] = 512;
					Tabl[83] = 482;
					Tabl[84] = 453;
					Tabl[85] = 430;
					Tabl[86] = 409;
					Tabl[87] = 390;
					Tabl[88] = 746;
					Tabl[89] = 714;
					Tabl[90] = 696;
					Tabl[91] = 650;
					Tabl[92] = 620;
					Tabl[93] = 580;
					Tabl[94] = 544;
					Tabl[95] = 513;
					Tabl[96] = 485;
					Tabl[97] = 461;
					Tabl[98] = 439;
					Tabl[99] = 816;
					Tabl[100] = 783;
					Tabl[101] = 769;
					Tabl[102] = 717;
					Tabl[103] = 674;
					Tabl[104] = 630;
					Tabl[105] = 611;
					Tabl[106] = 575;
					Tabl[107] = 543;
					Tabl[108] = 514;
					Tabl[109] = 490;
					Tabl[110] = 888;
					Tabl[111] = 852;
					Tabl[112] = 841;
					Tabl[113] = 785;
					Tabl[114] = 738;
					Tabl[115] = 690;
					Tabl[116] = 655;
					Tabl[117] = 643;
					Tabl[118] = 604;
					Tabl[119] = 572;
					Tabl[120] = 543;
					Tabl[121] = 974;
					Tabl[122] = 932;
					Tabl[123] = 913;
					Tabl[124] = 852;
					Tabl[125] = 801;
					Tabl[126] = 749;
					Tabl[127] = 712;
					Tabl[128] = 679;
					Tabl[129] = 641;
					Tabl[130] = 608;
					Tabl[131] = 563;


					break;
				case 5:
					Tabl[0] = 189.0;
					Tabl[1] = 156.0;
					Tabl[2] = 130.0;
					Tabl[3] = 108.0;
					Tabl[4] = 92.0;
					Tabl[5] = 81.0;
					Tabl[6] = 74.0;
					Tabl[7] = 70.0;
					Tabl[8] = 72.0;
					Tabl[9] = 79.0;
					Tabl[10] = 92.0;
					Tabl[11] = 245.0;
					Tabl[12] = 215.0;
					Tabl[13] = 190.0;
					Tabl[14] = 170.0;
					Tabl[15] = 154.0;
					Tabl[16] = 141.0;
					Tabl[17] = 131.0;
					Tabl[18] = 125.0;
					Tabl[19] = 124.0;
					Tabl[20] = 126.0;
					Tabl[21] = 133.0;
					Tabl[22] = 304.0;
					Tabl[23] = 275.0;
					Tabl[24] = 252.0;
					Tabl[25] = 232.0;
					Tabl[26] = 215.0;
					Tabl[27] = 200.0;
					Tabl[28] = 189.0;
					Tabl[29] = 180.0;
					Tabl[30] = 176.0;
					Tabl[31] = 175.0;
					Tabl[32] = 178.0;
					Tabl[33] = 470.0;
					Tabl[34] = 341.0;
					Tabl[35] = 316.0;
					Tabl[36] = 295.0;
					Tabl[37] = 276.0;
					Tabl[38] = 260.0;
					Tabl[39] = 247.0;
					Tabl[40] = 236.0;
					Tabl[41] = 229.0;
					Tabl[42] = 225.0;
					Tabl[43] = 223.0;
					Tabl[44] = 451.0;
					Tabl[45] = 416.0;
					Tabl[46] = 386.0;
					Tabl[47] = 360.0;
					Tabl[48] = 339.0;
					Tabl[49] = 320.0;
					Tabl[50] = 304.0;
					Tabl[51] = 291.0;
					Tabl[52] = 281.0;
					Tabl[53] = 274.0;
					Tabl[54] = 269.0;
					Tabl[55] = 562.0;
					Tabl[56] = 512.0;
					Tabl[57] = 470.0;
					Tabl[58] = 435.0;
					Tabl[59] = 405.0;
					Tabl[60] = 381.0;
					Tabl[61] = 362.0;
					Tabl[62] = 346.0;
					Tabl[63] = 333.0;
					Tabl[64] = 323.0;
					Tabl[65] = 315.0;
					Tabl[66] = 713.0;
					Tabl[67] = 636.0;
					Tabl[68] = 573.0;
					Tabl[69] = 521.0;
					Tabl[70] = 479.0;
					Tabl[71] = 447.0;
					Tabl[72] = 421.0;
					Tabl[73] = 401.0;
					Tabl[74] = 385.0;
					Tabl[75] = 372.0;
					Tabl[76] = 360.0;
					Tabl[77] = 990.0;
					Tabl[78] = 847.0;
					Tabl[79] = 729.0;
					Tabl[80] = 636.0;
					Tabl[81] = 568.0;
					Tabl[82] = 519.0;
					Tabl[83] = 485.0;
					Tabl[84] = 458.0;
					Tabl[85] = 438.0;
					Tabl[86] = 421.0;
					Tabl[87] = 405.0;
					Tabl[88] = 1110.0;
					Tabl[89] = 970.0;
					Tabl[90] = 964.0;
					Tabl[91] = 803.0;
					Tabl[92] = 691.0;
					Tabl[93] = 607.0;
					Tabl[94] = 558.0;
					Tabl[95] = 520.0;
					Tabl[96] = 494.0;
					Tabl[97] = 473.0;
					Tabl[98] = 454.0;
					Tabl[99] = 1110.0;
					Tabl[100] = 1150.0;
					Tabl[101] = 1094.0;
					Tabl[102] = 1028.0;
					Tabl[103] = 820.0;
					Tabl[104] = 719.0;
					Tabl[105] = 642.0;
					Tabl[106] = 589.0;
					Tabl[107] = 554.0;
					Tabl[108] = 527.0;
					Tabl[109] = 503.0;
					Tabl[110] = 1110.0;
					Tabl[111] = 1150.0;
					Tabl[112] = 1240.0;
					Tabl[113] = 1256.0;
					Tabl[114] = 1013.0;
					Tabl[115] = 825.0;
					Tabl[116] = 735.0;
					Tabl[117] = 667.0;
					Tabl[118] = 621.0;
					Tabl[119] = 584.0;
					Tabl[120] = 554.0;
					Tabl[121] = 1110.0;
					Tabl[122] = 1150.0;
					Tabl[123] = 1240.0;
					Tabl[124] = 1256.0;
					Tabl[125] = 1270.0;
					Tabl[126] = 925.0;
					Tabl[127] = 837.0;
					Tabl[128] = 754.0;
					Tabl[129] = 695.0;
					Tabl[130] = 644.0;
					Tabl[131] = 607.0;
					break;
				case 6:
					Tabl[0] = 162.0;
					Tabl[1] = 145.0;
					Tabl[2] = 120.0;
					Tabl[3] = 118.0;
					Tabl[4] = 108.0;
					Tabl[5] = 102.0;
					Tabl[6] = 99.0;
					Tabl[7] = 98.0;
					Tabl[8] = 99.0;
					Tabl[9] = 104.0;
					Tabl[10] = 108.0;
					Tabl[11] = 218.0;
					Tabl[12] = 190.0;
					Tabl[13] = 180.0;
					Tabl[14] = 165.0;
					Tabl[15] = 153.0;
					Tabl[16] = 145.0;
					Tabl[17] = 139.0;
					Tabl[18] = 135.0;
					Tabl[19] = 133.0;
					Tabl[20] = 134.0;
					Tabl[21] = 133.0;
					Tabl[22] = 280.0;
					Tabl[23] = 258.0;
					Tabl[24] = 241.0;
					Tabl[25] = 226.0;
					Tabl[26] = 212.0;
					Tabl[27] = 200.0;
					Tabl[28] = 191.0;
					Tabl[29] = 184.0;
					Tabl[30] = 177.0;
					Tabl[31] = 171.0;
					Tabl[32] = 166.0;
					Tabl[33] = 359.0;
					Tabl[34] = 337.0;
					Tabl[35] = 314.0;
					Tabl[36] = 294.0;
					Tabl[37] = 276.0;
					Tabl[38] = 260.0;
					Tabl[39] = 248.0;
					Tabl[40] = 237.0;
					Tabl[41] = 228.0;
					Tabl[42] = 219.0;
					Tabl[43] = 212.0;
					Tabl[44] = 455.0;
					Tabl[45] = 419.0;
					Tabl[46] = 389.0;
					Tabl[47] = 362.0;
					Tabl[48] = 339.0;
					Tabl[49] = 320.0;
					Tabl[50] = 304.0;
					Tabl[51] = 290.0;
					Tabl[52] = 278.0;
					Tabl[53] = 267.0;
					Tabl[54] = 258.0;
					Tabl[55] = 549.0;
					Tabl[56] = 505.0;
					Tabl[57] = 467.0;
					Tabl[58] = 431.0;
					Tabl[59] = 404.0;
					Tabl[60] = 381.0;
					Tabl[61] = 361.0;
					Tabl[62] = 344.0;
					Tabl[63] = 329.0;
					Tabl[64] = 315.0;
					Tabl[65] = 304.0;
					Tabl[66] = 654.0;
					Tabl[67] = 594.0;
					Tabl[68] = 542.0;
					Tabl[69] = 500.0;
					Tabl[70] = 469.0;
					Tabl[71] = 442.0;
					Tabl[72] = 419.0;
					Tabl[73] = 398.0;
					Tabl[74] = 380.0;
					Tabl[75] = 364.0;
					Tabl[76] = 340.0;
					Tabl[77] = 781.0;
					Tabl[78] = 693.0;
					Tabl[79] = 626.0;
					Tabl[80] = 575.0;
					Tabl[81] = 532.0;
					Tabl[82] = 502.0;
					Tabl[83] = 476.0;
					Tabl[84] = 453.0;
					Tabl[85] = 432.0;
					Tabl[86] = 413.0;
					Tabl[87] = 397.0;
					Tabl[88] = 928.0;
					Tabl[89] = 821.0;
					Tabl[90] = 732.0;
					Tabl[91] = 661.0;
					Tabl[92] = 608.0;
					Tabl[93] = 569.0;
					Tabl[94] = 539.0;
					Tabl[95] = 512.0;
					Tabl[96] = 488.0;
					Tabl[97] = 466.0;
					Tabl[98] = 444.0;
					Tabl[99] = 1110.0;
					Tabl[100] = 930.0;
					Tabl[101] = 796.0;
					Tabl[102] = 788.0;
					Tabl[103] = 715.0;
					Tabl[104] = 656.0;
					Tabl[105] = 614.0;
					Tabl[106] = 578.0;
					Tabl[107] = 549.0;
					Tabl[108] = 523.0;
					Tabl[109] = 499.0;
					Tabl[110] = 1110.0;
					Tabl[111] = 1150.0;
					Tabl[112] = 1076.0;
					Tabl[113] = 956.0;
					Tabl[114] = 853.0;
					Tabl[115] = 767.0;
					Tabl[116] = 698.0;
					Tabl[117] = 646.0;
					Tabl[118] = 613.0;
					Tabl[119] = 586.0;
					Tabl[120] = 560.0;
					Tabl[121] = 1110.0;
					Tabl[122] = 1150.0;
					Tabl[123] = 1240.0;
					Tabl[124] = 1165.0;
					Tabl[125] = 1022.0;
					Tabl[126] = 902.0;
					Tabl[127] = 791.0;
					Tabl[128] = 716.0;
					Tabl[129] = 680.0;
					Tabl[130] = 655.0;
					Tabl[131] = 627.0;
					break;
				case 7:
					Tabl[0] = 148.0;
					Tabl[1] = 127.0;
					Tabl[2] = 109.0;
					Tabl[3] = 96.0;
					Tabl[4] = 86.0;
					Tabl[5] = 80.0;
					Tabl[6] = 77.0;
					Tabl[7] = 77.0;
					Tabl[8] = 80.0;
					Tabl[9] = 86.0;
					Tabl[10] = 95.0;
					Tabl[11] = 209.0;
					Tabl[12] = 191.0;
					Tabl[13] = 176.0;
					Tabl[14] = 160.0;
					Tabl[15] = 146.0;
					Tabl[16] = 137.0;
					Tabl[17] = 132.0;
					Tabl[18] = 128.0;
					Tabl[19] = 124.0;
					Tabl[20] = 121.0;
					Tabl[21] = 119.0;
					Tabl[22] = 291.0;
					Tabl[23] = 269.0;
					Tabl[24] = 250.0;
					Tabl[25] = 230.0;
					Tabl[26] = 213.0;
					Tabl[27] = 201.0;
					Tabl[28] = 191.0;
					Tabl[29] = 182.0;
					Tabl[30] = 175.0;
					Tabl[31] = 168.0;
					Tabl[32] = 164.0;
					Tabl[33] = 378.0;
					Tabl[34] = 348.0;
					Tabl[35] = 324.0;
					Tabl[36] = 299.0;
					Tabl[37] = 278.0;
					Tabl[38] = 262.0;
					Tabl[39] = 249.0;
					Tabl[40] = 236.0;
					Tabl[41] = 225.0;
					Tabl[42] = 216.0;
					Tabl[43] = 209.0;
					Tabl[44] = 467.0;
					Tabl[45] = 431.0;
					Tabl[46] = 400.0;
					Tabl[47] = 370.0;
					Tabl[48] = 344.0;
					Tabl[49] = 323.0;
					Tabl[50] = 306.0;
					Tabl[51] = 290.0;
					Tabl[52] = 276.0;
					Tabl[53] = 264.0;
					Tabl[54] = 254.0;
					Tabl[55] = 564.0;
					Tabl[56] = 519.0;
					Tabl[57] = 480.0;
					Tabl[58] = 444.0;
					Tabl[59] = 412.0;
					Tabl[60] = 386.0;
					Tabl[61] = 363.0;
					Tabl[62] = 344.0;
					Tabl[63] = 326.0;
					Tabl[64] = 311.0;
					Tabl[65] = 299.0;
					Tabl[66] = 673.0;
					Tabl[67] = 612.0;
					Tabl[68] = 561.0;
					Tabl[69] = 522.0;
					Tabl[70] = 484.0;
					Tabl[71] = 451.0;
					Tabl[72] = 423.0;
					Tabl[73] = 398.0;
					Tabl[74] = 378.0;
					Tabl[75] = 359.0;
					Tabl[76] = 344.0;
					Tabl[77] = 796.0;
					Tabl[78] = 715.0;
					Tabl[79] = 649.0;
					Tabl[80] = 602.0;
					Tabl[81] = 561.0;
					Tabl[82] = 520.0;
					Tabl[83] = 486.0;
					Tabl[84] = 455.0;
					Tabl[85] = 430.0;
					Tabl[86] = 408.0;
					Tabl[87] = 391.0;
					Tabl[88] = 929.0;
					Tabl[89] = 827.0;
					Tabl[90] = 744.0;
					Tabl[91] = 687.0;
					Tabl[92] = 640.0;
					Tabl[93] = 596.0;
					Tabl[94] = 552.0;
					Tabl[95] = 517.0;
					Tabl[96] = 486.0;
					Tabl[97] = 460.0;
					Tabl[98] = 442.0;
					Tabl[99] = 1069.0;
					Tabl[100] = 942.0;
					Tabl[101] = 844.0;
					Tabl[102] = 775.0;
					Tabl[103] = 722.0;
					Tabl[104] = 675.0;
					Tabl[105] = 628.0;
					Tabl[106] = 587.0;
					Tabl[107] = 550.0;
					Tabl[108] = 518.0;
					Tabl[109] = 495.0;
					Tabl[110] = 1110.0;
					Tabl[111] = 1042.0;
					Tabl[112] = 944.0;
					Tabl[113] = 865.0;
					Tabl[114] = 805.0;
					Tabl[115] = 756.0;
					Tabl[116] = 709.0;
					Tabl[117] = 663.0;
					Tabl[118] = 618.0;
					Tabl[119] = 581.0;
					Tabl[120] = 555.0;
					Tabl[121] = 1110.0;
					Tabl[122] = 1127.0;
					Tabl[123] = 1044.0;
					Tabl[124] = 957.0;
					Tabl[125] = 889.0;
					Tabl[126] = 839.0;
					Tabl[127] = 795.0;
					Tabl[128] = 754.0;
					Tabl[129] = 692.0;
					Tabl[130] = 645.0;
					Tabl[131] = 623.0;
					break;
				case 8:
					Tabl[0] = 189.0;
					Tabl[1] = 158.0;
					Tabl[2] = 132.0;
					Tabl[3] = 111.0;
					Tabl[4] = 95.0;
					Tabl[5] = 84.0;
					Tabl[6] = 77.0;
					Tabl[7] = 74.0;
					Tabl[8] = 76.0;
					Tabl[9] = 80.0;
					Tabl[10] = 87.0;
					Tabl[11] = 260.0;
					Tabl[12] = 229.0;
					Tabl[13] = 202.0;
					Tabl[14] = 178.0;
					Tabl[15] = 159.0;
					Tabl[16] = 144.0;
					Tabl[17] = 135.0;
					Tabl[18] = 129.0;
					Tabl[19] = 127.0;
					Tabl[20] = 127.0;
					Tabl[21] = 125.0;
					Tabl[22] = 341.0;
					Tabl[23] = 304.0;
					Tabl[24] = 272.0;
					Tabl[25] = 245.0;
					Tabl[26] = 222.0;
					Tabl[27] = 205.0;
					Tabl[28] = 192.0;
					Tabl[29] = 184.0;
					Tabl[30] = 179.0;
					Tabl[31] = 176.0;
					Tabl[32] = 172.0;
					Tabl[33] = 442.0;
					Tabl[34] = 393.0;
					Tabl[35] = 351.0;
					Tabl[36] = 314.0;
					Tabl[37] = 286.0;
					Tabl[38] = 266.0;
					Tabl[39] = 250.0;
					Tabl[40] = 238.0;
					Tabl[41] = 230.0;
					Tabl[42] = 225.0;
					Tabl[43] = 219.0;
					Tabl[44] = 570.0;
					Tabl[45] = 499.0;
					Tabl[46] = 436.0;
					Tabl[47] = 387.0;
					Tabl[48] = 352.0;
					Tabl[49] = 327.0;
					Tabl[50] = 308.0;
					Tabl[51] = 293.0;
					Tabl[52] = 282.0;
					Tabl[53] = 274.0;
					Tabl[54] = 266.0;
					Tabl[55] = 713.0;
					Tabl[56] = 614.0;
					Tabl[57] = 532.0;
					Tabl[58] = 469.0;
					Tabl[59] = 423.0;
					Tabl[60] = 390.0;
					Tabl[61] = 365.0;
					Tabl[62] = 347.0;
					Tabl[63] = 333.0;
					Tabl[64] = 323.0;
					Tabl[65] = 314.0;
					Tabl[66] = 883.0;
					Tabl[67] = 752.0;
					Tabl[68] = 620.0;
					Tabl[69] = 553.0;
					Tabl[70] = 500.0;
					Tabl[71] = 456.0;
					Tabl[72] = 420.0;
					Tabl[73] = 400.0;
					Tabl[74] = 384.0;
					Tabl[75] = 372.0;
					Tabl[76] = 363.0;
					Tabl[77] = 1080.0;
					Tabl[78] = 910.0;
					Tabl[79] = 717.0;
					Tabl[80] = 640.0;
					Tabl[81] = 576.0;
					Tabl[82] = 524.0;
					Tabl[83] = 482.0;
					Tabl[84] = 453.0;
					Tabl[85] = 433.0;
					Tabl[86] = 418.0;
					Tabl[87] = 408.0;
					Tabl[88] = 1110.0;
					Tabl[89] = 1086.0;
					Tabl[90] = 810.0;
					Tabl[91] = 734.0;
					Tabl[92] = 668.0;
					Tabl[93] = 603.0;
					Tabl[94] = 550.0;
					Tabl[95] = 512.0;
					Tabl[96] = 486.0;
					Tabl[97] = 465.0;
					Tabl[98] = 449.0;
					Tabl[99] = 1110.0;
					Tabl[100] = 1150.0;
					Tabl[101] = 908.0;
					Tabl[102] = 835.0;
					Tabl[103] = 764.0;
					Tabl[104] = 691.0;
					Tabl[105] = 628.0;
					Tabl[106] = 579.0;
					Tabl[107] = 544.0;
					Tabl[108] = 516.0;
					Tabl[109] = 495.0;
					Tabl[110] = 1110.0;
					Tabl[111] = 1150.0;
					Tabl[112] = 1005.0;
					Tabl[113] = 924.0;
					Tabl[114] = 850.0;
					Tabl[115] = 777.0;
					Tabl[116] = 710.0;
					Tabl[117] = 655.0;
					Tabl[118] = 611.0;
					Tabl[119] = 572.0;
					Tabl[120] = 538.0;
					Tabl[121] = 1110.0;
					Tabl[122] = 1150.0;
					Tabl[123] = 1102.0;
					Tabl[124] = 1010.0;
					Tabl[125] = 932.0;
					Tabl[126] = 861.0;
					Tabl[127] = 795.0;
					Tabl[128] = 736.0;
					Tabl[129] = 682.0;
					Tabl[130] = 635.0;
					Tabl[131] = 595.0;
					break;
				case 9:
					Tabl[0] = 178.7;
					Tabl[1] = 161.1;
					Tabl[2] = 142.0;
					Tabl[3] = 122.9;
					Tabl[4] = 108.0;
					Tabl[5] = 96.7;
					Tabl[6] = 90.3;
					Tabl[7] = 85.6;
					Tabl[8] = 82.5;
					Tabl[9] = 77.5;
					Tabl[10] = 73.3;

					// 20 ÌÂò
					Tabl[11] = 252.6;
					Tabl[12] = 230.2;
					Tabl[13] = 208.4;
					Tabl[14] = 187.4;
					Tabl[15] = 169.8;
					Tabl[16] = 155.9;
					Tabl[17] = 145.9;
					Tabl[18] = 137.5;
					Tabl[19] = 131.0;
					Tabl[20] = 124.5;
					Tabl[21] = 119.3;

					// 30 ÌÂò
					Tabl[22] = 326.6;
					Tabl[23] = 299.3;
					Tabl[24] = 274.8;
					Tabl[25] = 251.9;
					Tabl[26] = 231.6;
					Tabl[27] = 215.0;
					Tabl[28] = 201.6;
					Tabl[29] = 189.5;
					Tabl[30] = 179.6;
					Tabl[31] = 171.6;
					Tabl[32] = 165.4;

					// 40 ÌÂò
					Tabl[33] = 401.5;
					Tabl[34] = 369.1;
					Tabl[35] = 341.0;
					Tabl[36] = 315.8;
					Tabl[37] = 292.8;
					Tabl[38] = 273.6;
					Tabl[39] = 257.2;
					Tabl[40] = 241.5;
					Tabl[41] = 228.2;
					Tabl[42] = 218.6;
					Tabl[43] = 211.5;

					// 50 ÌÂò
					Tabl[44] = 563.1;
					Tabl[45] = 518.5;
					Tabl[46] = 479.7;
					Tabl[47] = 445.6;
					Tabl[48] = 414.5;
					Tabl[49] = 388.3;
					Tabl[50] = 366.7;
					Tabl[51] = 347.7;
					Tabl[52] = 329.5;
					Tabl[53] = 314.8;
					Tabl[54] = 304.7;

					// 60 ÌÂò
					Tabl[55] = 567.0;
					Tabl[56] = 521.0;
					Tabl[57] = 480.0;
					Tabl[58] = 443.0;
					Tabl[59] = 411.0;
					Tabl[60] = 387.0;
					Tabl[61] = 366.0;
					Tabl[62] = 347.0;
					Tabl[63] = 333.0;
					Tabl[64] = 322.0;
					Tabl[65] = 312.0;

					// 70 ÌÂò
					Tabl[66] = 650.0;
					Tabl[67] = 597.9;
					Tabl[68] = 552.8;
					Tabl[69] = 513.5;
					Tabl[70] = 479.2;
					Tabl[71] = 447.9;
					Tabl[72] = 422.6;
					Tabl[73] = 401.6;
					Tabl[74] = 381.8;
					Tabl[75] = 364.5;
					Tabl[76] = 352.6;

					// 80 ÌÂò
					Tabl[77] = 753.0;
					Tabl[78] = 686.9;
					Tabl[79] = 632.3;
					Tabl[80] = 584.3;
					Tabl[81] = 544.7;
					Tabl[82] = 510.1;
					Tabl[83] = 480.9;
					Tabl[84] = 456.0;
					Tabl[85] = 433.7;
					Tabl[86] = 414.5;
					Tabl[87] = 401.0;

					// 90 ÌÂò
					Tabl[88] = 878.7;
					Tabl[89] = 801.3;
					Tabl[90] = 728.3;
					Tabl[91] = 670.6;
					Tabl[92] = 618.4;
					Tabl[93] = 577.5;
					Tabl[94] = 542.1;
					Tabl[95] = 512.9;
					Tabl[96] = 486.8;
					Tabl[97] = 465.0;
					Tabl[98] = 449.7;

					// 100 ÌÂò
					Tabl[99] = 1005.4;
					Tabl[100] = 924.90;
					Tabl[101] = 845.2;
					Tabl[102] = 773.9;
					Tabl[103] = 710.1;
					Tabl[104] = 657.8;
					Tabl[105] = 613.2;
					Tabl[106] = 576.3;
					Tabl[107] = 544.5;
					Tabl[108] = 518.9;
					Tabl[109] = 501.1;

					// 110 ÌÂò
					Tabl[110] = 1132.0;
					Tabl[111] = 1048.0;
					Tabl[112] = 963.0;
					Tabl[113] = 889.8;
					Tabl[114] = 821.5;
					Tabl[115] = 751.3;
					Tabl[116] = 700.0;
					Tabl[117] = 653.9;
					Tabl[118] = 613.7;
					Tabl[119] = 580.6;
					Tabl[120] = 557.4;

					// 120 ÌÂò
					Tabl[121] = 1258.7;
					Tabl[122] = 1172.8;
					Tabl[123] = 1080.8;
					Tabl[124] = 1006.2;
					Tabl[125] = 938.6;
					Tabl[126] = 848.8;
					Tabl[127] = 790.8;
					Tabl[128] = 736.3;
					Tabl[129] = 687.1;
					Tabl[130] = 645.1;
					Tabl[131] = 615.3;
					break;
				case 10:
					Tabl[0] = 179.0;
					Tabl[1] = 154.0;
					Tabl[2] = 135.0;
					Tabl[3] = 122.0;
					Tabl[4] = 111.0;
					Tabl[5] = 104.0;
					Tabl[6] = 101.0;
					Tabl[7] = 100.0;
					Tabl[8] = 104.0;
					Tabl[9] = 110.0;
					Tabl[10] = 119.0;
					Tabl[11] = 268.0;
					Tabl[12] = 234.0;
					Tabl[13] = 207.0;
					Tabl[14] = 187.0;
					Tabl[15] = 172.0;
					Tabl[16] = 160.0;
					Tabl[17] = 152.0;
					Tabl[18] = 146.0;
					Tabl[19] = 145.0;
					Tabl[20] = 146.0;
					Tabl[21] = 149.0;
					Tabl[22] = 357.0;
					Tabl[23] = 313.0;
					Tabl[24] = 278.0;
					Tabl[25] = 253.0;
					Tabl[26] = 233.0;
					Tabl[27] = 218.0;
					Tabl[28] = 205.0;
					Tabl[29] = 196.0;
					Tabl[30] = 190.0;
					Tabl[31] = 186.0;
					Tabl[32] = 183.0;
					Tabl[33] = 447.0;
					Tabl[34] = 393.0;
					Tabl[35] = 350.0;
					Tabl[36] = 318.0;
					Tabl[37] = 294.0;
					Tabl[38] = 276.0;
					Tabl[39] = 260.0;
					Tabl[40] = 247.0;
					Tabl[41] = 238.0;
					Tabl[42] = 230.0;
					Tabl[43] = 225.0;
					Tabl[44] = 535.0;
					Tabl[45] = 476.0;
					Tabl[46] = 430.0;
					Tabl[47] = 392.0;
					Tabl[48] = 361.0;
					Tabl[49] = 336.0;
					Tabl[50] = 316.0;
					Tabl[51] = 299.0;
					Tabl[52] = 286.0;
					Tabl[53] = 275.0;
					Tabl[54] = 266.0;
					Tabl[55] = 638.0;
					Tabl[56] = 567.0;
					Tabl[57] = 510.0;
					Tabl[58] = 464.0;
					Tabl[59] = 426.0;
					Tabl[60] = 396.0;
					Tabl[61] = 372.0;
					Tabl[62] = 352.0;
					Tabl[63] = 335.0;
					Tabl[64] = 321.0;
					Tabl[65] = 309.0;
					Tabl[66] = 740.0;
					Tabl[67] = 658.0;
					Tabl[68] = 591.0;
					Tabl[69] = 537.0;
					Tabl[70] = 492.0;
					Tabl[71] = 457.0;
					Tabl[72] = 430.0;
					Tabl[73] = 406.0;
					Tabl[74] = 387.0;
					Tabl[75] = 370.0;
					Tabl[76] = 355.0;
					Tabl[77] = 845.0;
					Tabl[78] = 753.0;
					Tabl[79] = 676.0;
					Tabl[80] = 614.0;
					Tabl[81] = 563.0;
					Tabl[82] = 522.0;
					Tabl[83] = 490.0;
					Tabl[84] = 463.0;
					Tabl[85] = 441.0;
					Tabl[86] = 420.0;
					Tabl[87] = 402.0;
					Tabl[88] = 947.0;
					Tabl[89] = 852.0;
					Tabl[90] = 769.0;
					Tabl[91] = 698.0;
					Tabl[92] = 639.0;
					Tabl[93] = 592.0;
					Tabl[94] = 555.0;
					Tabl[95] = 524.0;
					Tabl[96] = 497.0;
					Tabl[97] = 474.0;
					Tabl[98] = 453.0;
					Tabl[99] = 954.0;
					Tabl[100] = 954.0;
					Tabl[101] = 866.0;
					Tabl[102] = 789.0;
					Tabl[103] = 723.0;
					Tabl[104] = 668.0;
					Tabl[105] = 625.0;
					Tabl[106] = 588.0;
					Tabl[107] = 558.0;
					Tabl[108] = 531.0;
					Tabl[109] = 508.0;
					Tabl[110] = 954.0;
					Tabl[111] = 954.0;
					Tabl[112] = 917.0;
					Tabl[113] = 863.0;
					Tabl[114] = 807.0;
					Tabl[115] = 749.0;
					Tabl[116] = 699.0;
					Tabl[117] = 657.0;
					Tabl[118] = 623.0;
					Tabl[119] = 593.0;
					Tabl[120] = 568.0;
					Tabl[121] = 1110.0;
					Tabl[122] = 1150.0;
					Tabl[123] = 922.0;
					Tabl[124] = 920.0;
					Tabl[125] = 891.0;
					Tabl[126] = 835.0;
					Tabl[127] = 777.0;
					Tabl[128] = 731.0;
					Tabl[129] = 692.0;
					Tabl[130] = 660.0;
					Tabl[131] = 633.0;
					break;
			}
			return Tabl;
		}

		public static double getRashodGA(int ga, double P, double H) {
			double i, j, a, b, h, l, e, q, p, c, d, z, p1, p2, pp, x, x0, y;
			double[] Tabl = getTabl(ga);
			double Result = 0;

			if (P == 0) { Result = 0; } else {

				y = P;


				//  x = TI(4037, pTime);
				x = H;

				//  if(x > x0) x = x0;

				a = 13.0;
				b = 10.0;
				h = 1;
				l = 10;


				i = (int)((x - a) / h);
				j = (int)((y - b) / l);

				if (i < 1) i = 1;

				if (j < 1) j = 1;

				if (i > 9) i = 9;

				if (j > 10) j = 10;

				p = (x - a - (i * h)) / h;
				q = (y - b - (j * l)) / l;

				p1 = p * (p - 1) / 2.0;
				pp = (1 - (p * p));
				p2 = p * (p + 1) / 2.0;

				c = p1 * Tabl[(int)(((j - 1) * 11) + i - 1)] + pp * Tabl[(int)(((j - 1) * 11) + i)] + p2 * Tabl[(int)(((j - 1) * 11) + i + 1)];
				d = p1 * Tabl[(int)((j * 11) + i - 1)] + pp * Tabl[(int)((j * 11) + i)] + p2 * Tabl[(int)((j * 11) + i + 1)];
				e = p1 * Tabl[(int)(((j + 1) * 11) + i - 1)] + pp * Tabl[(int)(((j + 1) * 11) + i)] + p2 * Tabl[(int)(((j + 1) * 11) + i + 1)];

				z = (q * (q - 1) * c / 2.0) + ((1 - (q * q)) * d) + (q * (q + 1) * e / 2.0);

				Result = z;
			}
			return Result;
		}
	}
}
