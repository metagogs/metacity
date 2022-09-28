package message

import (
	"github.com/metagogs/gogs/session"
	"github.com/metagogs/metacity/model"
)

func SendJoinWorld(s *session.Session, in *model.JoinWorld) error {
	return s.SendMessage(in, "JoinWorld")
}

func SendJoinWorldSuccess(s *session.Session, in *model.JoinWorldSuccess) error {
	return s.SendMessage(in, "JoinWorldSuccess")
}

func SendJoinWorldNotifiy(s *session.Session, in *model.JoinWorldNotifiy) error {
	return s.SendMessage(in, "JoinWorldNotifiy")
}

func SendUpdateUserInWorld(s *session.Session, in *model.UpdateUserInWorld) error {
	return s.SendMessage(in, "UpdateUserInWorld")
}

func SendLeaveWorldNotifiy(s *session.Session, in *model.LeaveWorldNotifiy) error {
	return s.SendMessage(in, "LeaveWorldNotifiy")
}
