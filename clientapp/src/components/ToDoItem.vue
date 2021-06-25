<template>
  <!-- Index number, Description, Update button and Remove button for each ToDoItem-->
  <tr>
    <td class="otherItem">
      {{index}}:
    </td>
    <td class="descriptionBox">
      <label>{{dateTime}}</label><br/>
      <label>{{text}}</label>
    </td>
    <td class="otherItem">
      <button v-on:click="$emit('updateToDoItem')">Update</button>
    </td>
    <td class="otherItem">
      <button v-on:click="$emit('removeToDoItem')">Remove</button>
    </td>
  </tr>
</template>

<script>
  export default {
    name: 'ToDoItem',
    props: {
      // ToDoItem Description
      text: String,
      // ToDoItem Create Time
      dateTime: String,
      // ToDoItem Index
      index: Number
    }
  }
</script>

<style scoped>
td {
  margin: 10px;
}
button {
  margin: 0 40px 0 0;
}
</style>
