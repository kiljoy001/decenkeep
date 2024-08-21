package main

import (
    "C"
    "context"
    "github.com/codenotary/immudb/pkg/client"
    "log"
)

var immuClient client.ImmuClient

//export InitImmuClient
func InitImmuClient() {
	var err error
	immuClient, err = client.NewImmuClient(client.DefaultOptions())
	if err != nil {
		log.Fatalf("Failed to create immudb client: %v", err)
	}
}

//export Set
func Set(key *C.char, value *C.char) {
	k := C.GoString(key)
	v := C.GoString(value)
	_, err := immuClient.Set(context.Background(), []byte(k), []byte(v))
	if err != nil {
		log.Printf("Error setting key-value: %v", err)
	}
}

//export Get
func Get(key *C.char) *C.char {
	k := C.GoString(key)
	entry, err := immuClient.Get(context.Background(), []byte(k))
	if err != nil {
		log.Printf("Error getting key-value: %v", err)
		return nil
	}
	return C.CString(string(entry.Value))
}

func main() {}