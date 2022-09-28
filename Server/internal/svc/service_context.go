package svc

import (
	"github.com/bwmarrin/snowflake"
	"github.com/metagogs/gogs"
	"github.com/metagogs/gogs/group"
	"github.com/metagogs/gogs/utils/snow"
)

type ServiceContext struct {
	*gogs.App
	SF    *snowflake.Node
	Group *group.MemoryGroup
}

func NewServiceContext(app *gogs.App) *ServiceContext {
	sf, _ := snow.NewSnowNode()
	memGroup := group.NewMemoryGroup("world", sf.Generate().Int64())
	return &ServiceContext{
		App:   app,
		SF:    sf,
		Group: memGroup,
	}
}
