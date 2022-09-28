package model

import (
	"context"
	"reflect"

	"github.com/metagogs/gogs"
	"github.com/metagogs/gogs/component"
	"github.com/metagogs/gogs/packet"
	"github.com/metagogs/gogs/session"
)

func RegisterAllComponents(s *gogs.App, srv Component) {
	registerBaseWorldComponent(s, srv)

}

func registerBaseWorldComponent(s *gogs.App, srv Component) {
	s.RegisterComponent(_BaseWorldComponentDesc, srv)
}

type Component interface {
	JoinWorld(ctx context.Context, s *session.Session, in *JoinWorld)

	UpdateUserInWorld(ctx context.Context, s *session.Session, in *UpdateUserInWorld)
}

func _BaseWorldComponent_JoinWorld_Handler(srv interface{}, ctx context.Context, sess *session.Session, in interface{}) {
	srv.(Component).JoinWorld(ctx, sess, in.(*JoinWorld))
}

func _BaseWorldComponent_UpdateUserInWorld_Handler(srv interface{}, ctx context.Context, sess *session.Session, in interface{}) {
	srv.(Component).UpdateUserInWorld(ctx, sess, in.(*UpdateUserInWorld))
}

var _BaseWorldComponentDesc = component.ComponentDesc{
	ComonentName:   "BaseWorldComponent",
	ComponentIndex: 1, // equeal to module index
	ComponentType:  (*Component)(nil),
	Methods: []component.ComponentMethodDesc{
		{
			MethodIndex: packet.CreateAction(packet.ServicePacket, 1, 1), // 0x810001 8454145
			FieldType:   reflect.TypeOf(JoinWorld{}),
			Handler:     _BaseWorldComponent_JoinWorld_Handler,
			FiledHanler: func() interface{} {
				return new(JoinWorld)
			},
		},
		{
			MethodIndex: packet.CreateAction(packet.ServicePacket, 1, 2), // 0x810002 8454146
			FieldType:   reflect.TypeOf(JoinWorldSuccess{}),
			Handler:     nil,
			FiledHanler: func() interface{} {
				return new(JoinWorldSuccess)
			},
		},
		{
			MethodIndex: packet.CreateAction(packet.ServicePacket, 1, 3), // 0x810003 8454147
			FieldType:   reflect.TypeOf(JoinWorldNotifiy{}),
			Handler:     nil,
			FiledHanler: func() interface{} {
				return new(JoinWorldNotifiy)
			},
		},
		{
			MethodIndex: packet.CreateAction(packet.ServicePacket, 1, 4), // 0x810004 8454148
			FieldType:   reflect.TypeOf(UpdateUserInWorld{}),
			Handler:     _BaseWorldComponent_UpdateUserInWorld_Handler,
			FiledHanler: func() interface{} {
				return new(UpdateUserInWorld)
			},
		},
		{
			MethodIndex: packet.CreateAction(packet.ServicePacket, 1, 5), // 0x810005 8454149
			FieldType:   reflect.TypeOf(LeaveWorldNotifiy{}),
			Handler:     nil,
			FiledHanler: func() interface{} {
				return new(LeaveWorldNotifiy)
			},
		},
	},
}
