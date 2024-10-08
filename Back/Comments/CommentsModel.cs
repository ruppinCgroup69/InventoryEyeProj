﻿using InventoryEyeBack.Posts;
using InventoryEyeBack.Users;
using System.Data;

namespace InventoryEyeBack.Comments
{
    public class CommentsModel
    {
        int postId;
        int userId;
        string userName;
        string userImage;
        int storeId;
        int stockId;
        int commentId;
        DateTime createdAt;
        DateTime editedAt;
        string content;
        DateTime inventoryEye;
        string storeLocation;
        bool bought;
        DateTime boughtDate;
        int productQuality;
        int score;
        int satisfaction;

        public CommentsModel(int postId,int userId, int storeId, int stockId, int commentId, DateTime createdAt, DateTime editedAt, string content, DateTime inventoryEye, string storeLocation, bool bought, DateTime boughtDate, int productQuality, int satisfaction)
        {
            PostId = postId;
            UserId = userId;
            StoreId = storeId;
            StockId = stockId;
            CommentId = commentId;
            CreatedAt = createdAt;
            EditedAt = editedAt;
            Content = content;
            InventoryEye = inventoryEye;
            StoreLocation = storeLocation;
            Bought = bought;
            BoughtDate = boughtDate;
            ProductQuality = productQuality;
            Satisfaction = satisfaction;
        }

        public CommentsModel() { }

        public int PostId { get => postId; set => postId = value; }
        public int UserId { get => userId; set => userId = value; }
        public int StoreId { get => storeId; set => storeId = value; }
        public int StockId { get => stockId; set => stockId = value; }
        public int CommentId { get => commentId; set => commentId = value; }
        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }
        public DateTime EditedAt { get => editedAt; set => editedAt = value; }
        public string Content { get => content; set => content = value; }
        public DateTime InventoryEye { get => inventoryEye; set => inventoryEye = value; }
        public string StoreLocation { get => storeLocation; set => storeLocation = value; }
        public bool Bought { get => bought; set => bought = value; }
        public DateTime BoughtDate { get => boughtDate; set => boughtDate = value; }
        public int ProductQuality { get => productQuality; set => productQuality = value; }
        public string UserName { get => userName; set => userName = value; }
        public string UserImage { get => userImage; set => userImage = value; }
        public int Score { get => score; set => score = value; }
        public int Satisfaction { get => satisfaction; set => satisfaction = value; }

        public int InsertComment(CommentsModel comment)
        {
            CommentsDBS dbs = new CommentsDBS();
            return dbs.InsertCommentDBS(comment);
        }

        public int DeleteComment(int id)
        {
            CommentsDBS dbs = new CommentsDBS();
            return dbs.DeleteCommentDBS(id);
        }
        public CommentsModel ReadCommentByCommentId(int commentId)
        {
            CommentsDBS dbs = new CommentsDBS();
            return dbs.ReadCommentByCommentIdDBS(commentId);
        }

        public List<CommentsModel> ReadAllComments()
        {
            CommentsDBS dbs = new CommentsDBS();
            return dbs.ReadAllCommentsdDBS();
        }
         public List<CommentsModel> ReadCommentsByPostId(int postId)
         {
            CommentsDBS dbs = new CommentsDBS();
            return dbs.ReadCommentsByPostIdDBS(postId);
        }

        public List<CommentsModel> ReadCommentstByUserId(int userId)
    {
            CommentsDBS dbs = new CommentsDBS();
            return dbs.ReadCommentsByUserIdDBS(userId);
        }
        public int UpdateComment(CommentsModel comment)
        {
            CommentsDBS dbs = new CommentsDBS();
            return dbs.UpdateCommentDBS(comment);
        }
    }
}
