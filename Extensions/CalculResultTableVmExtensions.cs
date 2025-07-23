namespace Lopcommerce.Regles.WebAPI.Tests.Extensions
{
    using Lopcommerce.ApiRemuneration;
    using Lopcommerce.Regles.Models.Domain.Simulateur;
    public static class CalculResultTableVmExtensions
    {
        public static SimulateurRemunerationApprentiResult MapToSimulateurRemuneration(this CalculResultTableVm? calculResultTableVm)
        {
            var result = new SimulateurRemunerationApprentiResult();

            if (calculResultTableVm != null)
            {
                if (calculResultTableVm?.P1?.Rows?.Any() == true)
                {
                    if (calculResultTableVm.P1.Rows.Count > 0 && calculResultTableVm?.P1?.Rows.ElementAt(0) != null)
                    {
                        result.Annee1 = new SimulateurRemunerationApprentiPeriode
                        {
                            Du = DateTime.Parse(calculResultTableVm.P1.Rows.ElementAt(0).DateDebut),
                            Au = DateTime.Parse(calculResultTableVm.P1.Rows.ElementAt(0).DateFin),
                            Taux = (float?)calculResultTableVm.P1.Rows.ElementAt(0).Taux
                        };
                    }

                    if (calculResultTableVm.P1.Rows.Count > 1 && calculResultTableVm.P1.Rows.ElementAt(1) != null)
                    {
                        result.Annee2 = new SimulateurRemunerationApprentiPeriode
                        {
                            Du = DateTime.Parse(calculResultTableVm.P1.Rows.ElementAt(1).DateDebut),
                            Au = DateTime.Parse(calculResultTableVm.P1.Rows.ElementAt(1).DateFin),
                            Taux = (float?)calculResultTableVm.P1.Rows.ElementAt(1).Taux
                        };
                    }

                    if (calculResultTableVm.P1.Rows.Count > 2 && calculResultTableVm.P1.Rows.ElementAt(2) != null)
                    {
                        result.Annee3 = new SimulateurRemunerationApprentiPeriode
                        {
                            Du = DateTime.Parse(calculResultTableVm.P1.Rows.ElementAt(2).DateDebut),
                            Au = DateTime.Parse(calculResultTableVm.P1.Rows.ElementAt(2).DateFin),
                            Taux = (float?)calculResultTableVm.P1.Rows.ElementAt(2).Taux
                        };
                    }

                    if (calculResultTableVm.P1.Rows.Count > 3 && calculResultTableVm.P1.Rows.ElementAt(3) != null)
                    {
                        result.Annee4 = new SimulateurRemunerationApprentiPeriode
                        {
                            Du = DateTime.Parse(calculResultTableVm.P1.Rows.ElementAt(3).DateDebut),
                            Au = DateTime.Parse(calculResultTableVm.P1.Rows.ElementAt(3).DateFin),
                            Taux = (float?)calculResultTableVm.P1.Rows.ElementAt(3).Taux
                        };
                    }

                    result.SalaireBrut = (decimal)calculResultTableVm.P1.Rows.ElementAt(0).Salaire;
                    result.Smic = (decimal)calculResultTableVm.P1.Rows.ElementAt(0).Smic;
                }

                if (calculResultTableVm?.P2?.Rows?.Any() == true)
                {
                    if (calculResultTableVm.P2.Rows.Count > 0 && calculResultTableVm.P2.Rows.ElementAt(0) != null)
                    {
                        result.Annee1Bis = new SimulateurRemunerationApprentiPeriode
                        {
                            Du = DateTime.Parse(calculResultTableVm.P2.Rows.ElementAt(0).DateDebut),
                            Au = DateTime.Parse(calculResultTableVm.P2.Rows.ElementAt(0).DateFin),
                            Taux = (float?)calculResultTableVm.P2.Rows.ElementAt(0).Taux
                        };
                    }

                    if (calculResultTableVm.P2.Rows.Count > 1 && calculResultTableVm.P2.Rows.ElementAt(1) != null)
                    {
                        result.Annee2Bis = new SimulateurRemunerationApprentiPeriode
                        {
                            Du = DateTime.Parse(calculResultTableVm.P2.Rows.ElementAt(1).DateDebut),
                            Au = DateTime.Parse(calculResultTableVm.P2.Rows.ElementAt(1).DateFin),
                            Taux = (float?)calculResultTableVm.P2.Rows.ElementAt(1).Taux
                        };
                    }

                    if (calculResultTableVm.P2.Rows.Count > 2 && calculResultTableVm.P2.Rows.ElementAt(2) != null)
                    {
                        result.Annee3Bis = new SimulateurRemunerationApprentiPeriode
                        {
                            Du = DateTime.Parse(calculResultTableVm.P2.Rows.ElementAt(2).DateDebut),
                            Au = DateTime.Parse(calculResultTableVm.P2.Rows.ElementAt(2).DateFin),
                            Taux = (float?)calculResultTableVm.P2.Rows.ElementAt(2).Taux
                        };
                    }

                    if (calculResultTableVm.P2.Rows.Count > 3 && calculResultTableVm.P2.Rows.ElementAt(3) != null)
                    {
                        result.Annee4Bis = new SimulateurRemunerationApprentiPeriode
                        {
                            Du = DateTime.Parse(calculResultTableVm.P2.Rows.ElementAt(3).DateDebut),
                            Au = DateTime.Parse(calculResultTableVm.P2.Rows.ElementAt(3).DateFin),
                            Taux = (float?)calculResultTableVm.P2.Rows.ElementAt(3).Taux
                        };
                    }
                }

                return result;
            }

            return result;
        }
    }
}
