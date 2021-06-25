<template>
  <div class="ToDoList">
    <!-- Title, TextInput and add button -->
    <h1>{{ listName }}</h1>
    <input type="text" list="DataListItemsList" v-model="dataListItem"/>
    <datalist id="DataListItemsList">
      <option
        v-for="(dataListItem, index) in listOfItems"
        :key="index">
        {{index}}: {{dataListItem.text}}
      </option>
    </datalist>
    <button v-on:click="addToDoItem(dataListItem)">Add</button>

    <!-- Dialog to show helpful error message -->
    <div class="dialogView" id="itemDialogContainer">
        <div id="itemDialog"/>
        <button v-on:click="closeDialog()">Close</button>
    </div>
    <div class="dialogOverlay" id="dialogOverlay"/>

    <!-- Table to store ToDoList -->
    <table>
      <colgroup>
       <col class="otherItem" span="1">
       <col class="descriptionItem" span="1">
       <col class="otherItem" span="1">
       <col class="otherItem" span="1">
      </colgroup>
      <ToDoItem
        v-for="(item, index) in listOfItems"
        :key="index"
        :text="item.text"
        :dateTime="item.dateTime"
        :index="index"
        @removeToDoItem="removeToDoItem(index)"
        @updateToDoItem="updateToDoItem(index, dataListItem)">
      </ToDoItem>
    </table>
  </div>
</template>

<script>

import ToDoItem from "./ToDoItem.vue";
import { getAllToDoItems } from '../services/ToDoListService';

export default {
  name: 'ToDoList',
  data() {
    return {
      // All the TODOItem would be retrieved from local storage and referenced here
      listOfItems: [],
      // For TextInput to auto filled and update TODOItem
      dataListItem: ""
    }
  },
  methods: {
    // Add item
    addToDoItem(msg){
      this.listOfItems.push({ text: msg, dateTime: new Date().toLocaleString()});
      this.showDialog("TODO created successfully");
      this.saveFile();
    },
    // Delete item
    removeToDoItem(index) {
      this.listOfItems.splice(index, 1);
      this.showDialog("TODO removed successfully");
      this.saveFile();
    },
    // Update item
    updateToDoItem(index, msg){
      this.listOfItems[index].text = msg;
      this.showDialog("TODO updated successfully");
      this.saveFile();
    },
    // Show the dialog for CUD
    showDialog(msg){
      document.getElementById("dialogOverlay").style.display = "block";
      document.getElementById("itemDialog").innerHTML = msg + "<br/>";
      document.getElementById("itemDialogContainer").style.display = "block";
    },
    // Hide all the dialog
    closeDialog(){
      document.getElementById("dialogOverlay").style.display = "none";
      document.getElementById("itemDialogContainer").style.display = "none";
    },
    // Store current TODO items to local storage as JSON.
    saveFile(){
        const data = JSON.stringify(this.listOfItems)
        window.localStorage.setItem('listOfItems', data);
    },
    //Call API if local storage is null (first time running)
    getToDoItems() {
      getAllToDoItems().then(res => {
        if (res.length > 0) {
          this.listOfItems = JSON.parse(JSON.stringify(res));
        } else {
          this.listOfItems = [{ text: 'Foo', dateTime: new Date().toLocaleString() },
           { text: 'Bar', dateTime: new Date().toLocaleString() }];
        }
      });
    }
  },
  mounted() {
    //Initialize TODOLIST
    if (localStorage.getItem("listOfItems") === null)
      this.getToDoItems();
    else {
      this.listOfItems = JSON.parse(localStorage.getItem("listOfItems"));
    }
  },
  props: {
    // NAME of title
    listName: String
  },
    // Reused components
  components: {
    ToDoItem
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
table {
  margin: 40px 0 0 0;
}
input {
  width: 35%;
}
/* TODOList other column width */
.otherItem {
  width: 15%;
}
/* TODOList description column width */
.descriptionItem {
  width: 55%;
}
/* dialog */
.dialogView {
  position: fixed;
	top: 50%;
	left: 50%;
  width: auto;
  height: auto;
  padding: 4px;
  display: none;
  text-align: center;
  background: white;
  border: 3px solid;
  z-index: 200;
}
/* The overlay behind dialog */
.dialogOverlay {
  position: fixed;
  width: 100%;
  height: 100%;
  background: black;
  opacity: 0.3;
  top: 0;
  left: 0;
  display: none;
  z-index: 100;
}
</style>
