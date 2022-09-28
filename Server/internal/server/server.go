package server

import (
	"context"

	"github.com/metagogs/metacity/internal/logic/baseworld"
	"github.com/metagogs/metacity/model"

	"github.com/metagogs/gogs/session"
	"github.com/metagogs/metacity/internal/svc"
)

type Server struct {
	svcCtx *svc.ServiceContext
}

func NewServer(svcCtx *svc.ServiceContext) *Server {
	return &Server{
		svcCtx: svcCtx,
	}
}

func (gogs *Server) JoinWorld(ctx context.Context, s *session.Session, in *model.JoinWorld) {
	l := baseworld.NewJoinWorldLogic(ctx, gogs.svcCtx, s)
	l.Handler(in)
}

func (gogs *Server) UpdateUserInWorld(ctx context.Context, s *session.Session, in *model.UpdateUserInWorld) {
	l := baseworld.NewUpdateUserInWorldLogic(ctx, gogs.svcCtx, s)
	l.Handler(in)
}
