using MiniProjetS04.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiniProjetS04.Controllers
{
    public class AccueilController : Controller
    {
        
        public ActionResult Accueil()
        {
            OffreDao offreDao = new OffreDao();

            AccueilViewModel model = new AccueilViewModel();
            model.Offres = offreDao.findAll();
            return View(model);
        }

        public ActionResult Admin()
        {
            OffreDao oDao = new OffreDao();
            UtilisateurDao uDao = new UtilisateurDao();
            CandidatureDao cDao = new CandidatureDao();

            AccueilViewModel model = new AccueilViewModel();
            model.Utilisateurs = uDao.findAll();
            model.Offres = oDao.findAll();
            model.nbUtilisateurs = uDao.countUtilisateur();
            model.nbOffres = oDao.countOffre();
            model.nbCandidatures = cDao.countCandidature();

            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authentification(Utilisateur u)
        {
            UtilisateurDao dao = new UtilisateurDao();
            
            int id = dao.Authentification(u.Email, u.Psw);
            if (id != -1)
            {
                u = dao.findById(id);
                Session["userId"] = id;
                Session["statut"] = u.Statut;
                return RedirectToAction("Profile");
            }
            else
            {
                TempData["ErrorMessage"] = "Invalid email or password. Please try again.";
                return RedirectToAction("Login");
            }
        }
        public ActionResult Profile()
        {
            if(Session["userId"] == null)
            {
                return RedirectToAction("Login", new { err = "Please log in to access your profile." });
            }

            UtilisateurDao dao = new UtilisateurDao();
            CvDao cvDao = new CvDao();
            OffreDao offreDao = new OffreDao();
            CandidatureDao canDao = new CandidatureDao();
            CandidatDao canAtDao = new CandidatDao();

            ProfilViewModel model = new ProfilViewModel();
            model.utilisateur = dao.findById((int) Session["userId"]);
            model.cv = cvDao.findByUserId((int) Session["userId"]);
            model.offres = offreDao.findByUserId((int)Session["userId"]);
            model.candidatures = canDao.findByUserId((int)Session["userId"]);
            model.candidats = canAtDao.listCandidats((int)Session["userId"]);

            return View(model);
        }

        [HttpPost]
        public ActionResult Signup(Utilisateur u)
        {
            UtilisateurDao dao = new UtilisateurDao();
            if (dao.insert(u))
            {
                TempData["SuccessMessage"] = "Account created successfully. Please log in.";
                return RedirectToAction("Login");
            }
            else
            {
                TempData["ErrorMessage"] = "An error occurred while creating your account. Please try again.";
                return RedirectToAction("Login");
            }

        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Accueil");
        }

        [HttpPost]
        public ActionResult UpdateProfile(Utilisateur u)
        {
            UtilisateurDao dao = new UtilisateurDao();
            dao.update(u);
            return RedirectToAction("Profile");
        }

        [HttpPost]
        public ActionResult UpdateCv(Cv c)
        {
            CvDao dao = new CvDao();

            if (c.Id != 0)
            {
                dao.update(c);
                
            } else
            {
                dao.insert(c);
                
            }
                return RedirectToAction("Profile");
        }

        [HttpPost]
        public ActionResult UpdateOffre(Offre o)
        {
            OffreDao dao = new OffreDao();
            if (o.Id != 0)
            {
                dao.update(o);
            }
            else
            {
                dao.insert(o);
            }
            return RedirectToAction("Profile");
        }

        public ActionResult DeleteOffre(Offre o)
        {

            OffreDao dao = new OffreDao();
            dao.delete(o);

            return RedirectToAction("Profile");
        }

        public ActionResult Postuler(Candidature c)
        {
            CandidatureDao dao = new CandidatureDao();
            if(!dao.exist(c))
            {
                dao.insert(c);
            }

            return RedirectToAction("Accueil");
        }

        [HttpPost]
        public ActionResult adminDeleteUtilisateur(Utilisateur u)
        {
            UtilisateurDao dao = new UtilisateurDao();
            dao.delete(u);
            return RedirectToAction("Admin");
        }

        [HttpPost]
        public ActionResult adminDeleteOffre(Offre o)
        {
            OffreDao dao =new OffreDao();
            dao.delete(o);
            return RedirectToAction("Admin");
        }

        [HttpPost]
        public ActionResult UpdateStatutUtilisateur(Utilisateur u)
        {
            if(u.Id != 0 && !u.Statut.Equals(null))
            {
                UtilisateurDao dao = new UtilisateurDao();
                dao.updateStatutUtilisateur(u);
            }
            return RedirectToAction("Admin");
        }

        [HttpPost]
        public ActionResult UpdateStatutOffre(Offre o)
        {
            if(o.Id != 0 && !o.statut.Equals(null))
            {
                OffreDao dao = new OffreDao();
                dao.updateStatutOffre(o);
            }
            return RedirectToAction("Admin");
        }

        [HttpPost]
        public ActionResult updateStatutCandidature(Candidature c)
        {
            CandidatureDao dao = new CandidatureDao();
            dao.updateStatut(c);
            return RedirectToAction("Profile");
        }
        
        [HttpGet]
        public ActionResult Search(Search s)
        {
            SearchDao dao = new SearchDao();

            AccueilViewModel model = new AccueilViewModel();
            model.Offres = dao.search(s);

            return View(model);
        }

    }
}