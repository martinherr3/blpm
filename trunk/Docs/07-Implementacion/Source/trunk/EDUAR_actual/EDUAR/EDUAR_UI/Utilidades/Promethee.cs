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

		public decimal pesoCriterio { get; set; }
		public enumFuncionPreferencia tipoFuncion { get; set; }
		public decimal limitePreferencia { get; set; }
		public decimal limiteIndiferencia { get; set; }
		public decimal limiteSigma { get; set; }

		/// <summary>
		/// Obteners the sigma.
		/// </summary>
		/// <param name="configuracion">The configuracion.</param>
		/// <param name="diferencia">The diferencia.</param>
		/// <returns></returns>
		public static decimal obtenerSigma(Promethee configuracion, decimal diferencia)
		{
			decimal retorno = 0;
			switch (configuracion.tipoFuncion)
			{
				case enumFuncionPreferencia.None:
					break;
				case enumFuncionPreferencia.VerdaderoCriterio:
					if (diferencia == 0) retorno = 0;
					else retorno = 1;
					break;
				case enumFuncionPreferencia.CuasiCriterio:
					if (diferencia <= configuracion.limiteIndiferencia) retorno = 0;
					else retorno = 1;
					break;
				case enumFuncionPreferencia.PseudoCriterioConPreferenciaLineal:
					if (diferencia <= configuracion.limitePreferencia) retorno = (diferencia / configuracion.limitePreferencia);
					else retorno = 1;
					break;
				case enumFuncionPreferencia.LevelCriterio:
					if (diferencia <= configuracion.limiteIndiferencia) retorno = 0;
					else
					{
						if (configuracion.limiteIndiferencia < diferencia && diferencia <= configuracion.limitePreferencia) retorno = 1 / 2;
						else retorno = 1;
					}
					break;
				case enumFuncionPreferencia.CriterioConPreferenciaLinealYAreaDeIndiferencia:
					if (diferencia <= configuracion.limiteIndiferencia) retorno = 0;
					else
					{
						if (configuracion.limiteIndiferencia < diferencia && diferencia <= configuracion.limitePreferencia)
							retorno = (diferencia - configuracion.limiteIndiferencia) / (configuracion.limitePreferencia - configuracion.limiteIndiferencia);
						else retorno = 1;
					}
					break;
				case enumFuncionPreferencia.CriterioGaussiano:
					double valor = 0;
					double difEnDouble = Convert.ToDouble(diferencia);
					double limiteSigma = Convert.ToDouble(configuracion.limiteSigma);
					valor = ((-1) * Math.Pow(difEnDouble, 2)) / (2 * Math.Pow(limiteSigma, 2));
					retorno = Convert.ToDecimal(1 - Math.Exp(valor));
					break;
				default:
					break;
			}
			return retorno;
		}
	}
}