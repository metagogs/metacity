package baseworld

import (
	"context"

	"github.com/metagogs/gogs/gslog"
	"github.com/metagogs/gogs/session"
	"github.com/metagogs/metacity/internal/svc"
	"github.com/metagogs/metacity/model"
	"go.uber.org/zap"
)

type UpdateUserInWorldLogic struct {
	ctx     context.Context
	svcCtx  *svc.ServiceContext
	session *session.Session
	*zap.Logger
}

func NewUpdateUserInWorldLogic(ctx context.Context, svcCtx *svc.ServiceContext, sess *session.Session) *UpdateUserInWorldLogic {
	return &UpdateUserInWorldLogic{
		ctx:     ctx,
		svcCtx:  svcCtx,
		session: sess,
		Logger:  gslog.NewLog("update_user_in_world_logic"),
	}
}

func (l *UpdateUserInWorldLogic) Handler(in *model.UpdateUserInWorld) {
	in.Uid = l.session.UID()
	users := l.svcCtx.Group.GetUsers(l.ctx)
	session.BroadcastMessage(users, in, nil, l.session.UID())
}
