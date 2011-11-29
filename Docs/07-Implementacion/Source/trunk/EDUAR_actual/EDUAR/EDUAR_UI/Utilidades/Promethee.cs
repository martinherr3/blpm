using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDUAR_UI.Utilidades
{
	public class Promethee
	{
		public enum enumFuncionPreferencia : int
		{
			None = 0,
			VerdaderoCriterio = 1,
			CuasiCriterio = 2,
			PseudoCriterioConPreferenciaLineal = 3,
			LevelCriterio = 4,
			CriterioConPreferenciaLinealYAreaDeIndiferencia = 5,
			CriterioGaussiano = 6
		}
	}
}