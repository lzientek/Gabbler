﻿using System;
using System.Collections.Generic;
using System.Linq;
using Gabbler.Core;
using Gabbler.gApi.Models.Gabs;

namespace Gabbler.gApi.Helpers.ModelExtensions
{
    internal static class GabsExtensions
    {
        internal static GabsList ToGabsList(this List<Gab> gabs,int startNumber, int maxQuantity)
        {
            //creation de l'objet basic
            var gabList = new GabsList()
            {
                TotalGabs = gabs.Count,
                StartGab = startNumber,
                Gabs = new List<GabBasicModel>(),
            };

            //ajout du nombre de gabs voulu
            int compteur = 0;
            for (int i = 0; i < maxQuantity
                && (i + startNumber) < gabs.Count; i++)
            {
                compteur++;
                gabList.Gabs.Add(gabs[startNumber+i].ToGabBasicModel());
            }

            //nbde gab ajouté
            gabList.NbOfShownGabs = compteur;
            return gabList;
        }

        internal static IEnumerable<GabBasicModel> ToGabBasicModel(this IEnumerable<Gab> gabs)
        {
            return gabs.Select(ToGabBasicModel);
        } 
        internal static GabBasicModel ToGabBasicModel(this Gab gab)
        {
            return new GabBasicModel()
            {
                NbOfLikes = gab.Likes.Count,
                Content = gab.Message.TrimEnd(),
                NbOfComments = gab.Comments.Count,
                Id = gab.Id,
                User = gab.User.ToUserBasicModel(),
                Likes = gab.Likes.Select(l => l.User.Pseudo).ToArray(),

                CreationDate = gab.CreationDate
            };
        }
        internal static GabDetailModel ToGabDetailModel(this Gab gab)
        {
            return new GabDetailModel()
            {
                NbOfLikes = gab.Likes.Count,
                Content = gab.Message.TrimEnd(),
                NbOfComments = gab.Comments.Count,
                Id = gab.Id,
                User = gab.User.ToUserBasicModel(),
                CreationDate = gab.CreationDate,
                ModificationDate = gab.ModificationDate,
                Likes = gab.Likes.Select(l=>l.User.Pseudo).ToArray(),
            };
        }

        internal static Gab ToGab(this GabPostModel gab)
        {
            return new Gab()
            {
                CreationDate = DateTime.Now,
                Message = gab.Content,
            };
        }
    }
}