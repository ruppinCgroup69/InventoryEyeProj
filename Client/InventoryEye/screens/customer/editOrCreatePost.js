import { StyleSheet, Text, View, TouchableOpacity, Image, TextInput, ScrollView, Keyboard, TouchableWithoutFeedback } from 'react-native'
import React from 'react'
import { Feather } from '@expo/vector-icons'
import { FontAwesome5 } from '@expo/vector-icons';
import { MaterialIcons } from '@expo/vector-icons';
import { Ionicons } from '@expo/vector-icons';
import { FontAwesome } from '@expo/vector-icons';
import { Octicons } from '@expo/vector-icons';
import profileImage from '../../images/profileImage.jpg'
import { useNavigation } from '@react-navigation/native';

const DismissKeyboard = ({ children }) => (
  <TouchableWithoutFeedback onPress={() => Keyboard.dismiss()}>
    {children}
  </TouchableWithoutFeedback>
);

export default function EditOrCreatePost() {
  const navigation = useNavigation();
  const handleExit = () => {
    navigation.goBack();//לא מחזיר אותי למסך האחרון אלא רק לדף הבית
  };

  return (
    <View style={styles.container}>
      <View style={styles.top}>
        <View style={styles.exit}>
          <TouchableOpacity  onPress={handleExit}>
            <Feather name="x" size={30} color="#111851" />
          </TouchableOpacity>
        </View>
        <View style={styles.title}>
          <Text style={styles.createHeader}>Create New Post</Text>
        </View>
        <View style={styles.uploadIcon}>
          <TouchableOpacity>
            <Feather name="upload" size={30} color="#111851" />
          </TouchableOpacity>
        </View>
      </View>

      <View style={styles.center}>
        <View style={styles.profile}>
          <View style={styles.imageContainer}>
            <Image source={profileImage} style={styles.image} />
          </View>
          <View>
            <Text style={styles.userName}>Sharon Tebul</Text>
          </View>
        </View>

        <DismissKeyboard>
        <ScrollView style={styles.content}>
          <TextInput multiline style={styles.contentText} placeholder='What product are you looking for ?' />
        </ScrollView>
        </DismissKeyboard>
      </View>

      <View style={styles.bottom}>
        <View style={styles.iconItem}>
          <FontAwesome5 name="image" size={24} color="#111851" />
          <MaterialIcons name="category" size={24} color="#111851" />
          <FontAwesome name="pencil" size={24} color="#111851" />
          <Ionicons name="color-palette-outline" size={24} color="#111851" />
          <FontAwesome name="building-o" size={24} color="#111851" />
          <Octicons name="search" size={24} color="#111851" />
        </View>
        <View style={styles.inputItem}>
          <Text style={styles.imgText}>Image</Text>
          <Text style={styles.imgText}>category</Text>
          <TextInput style={styles.input} placeholder='product name' />
          <TextInput style={styles.input} placeholder='color' />
          <TextInput style={styles.input} placeholder='company' />
          <TextInput style={styles.input} placeholder='location' />
        </View>
      </View>
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#EAF0F3',
    alignItems: 'center',
    justifyContent: 'center',
  },
  top: {
    position: 'absolute',
    top: 0,
    flexDirection: 'row',
    height: '12%',
    width: '100%',
    backgroundColor: '#C8DFEA',
    marginTop: 40,
    alignItems: 'center',
    borderBottomColor: 'white',
    borderBottomWidth: '2'
  },
  exit: {
    width: '15%',
    height: '100%',
    justifyContent: 'center',
    alignItems: 'center',
  },
  title: {
    width: '70%',
    height: '100%',
    justifyContent: 'center',
    alignItems: 'center',
  },
  uploadIcon: {
    width: '15%',
    height: '100%',
    justifyContent: 'center',
    alignItems: 'center',
  },
  createHeader: {
    color: '#111851',
    fontWeight: 'bold',
    fontSize: 25,
  },
  center: {
    position: 'absolute',
    top: '12%',
    flexDirection: 'column',
    height: '44%',
    width: '100%',
    marginTop: 40,
    alignItems: 'center',
    borderBottomColor: '#D4D1D0',
    borderBottomWidth: '2',

  },
  profile: {
    flexDirection: 'row',
    width: '100%', // Take full width
    paddingTop: 10,
    alignItems: 'center',
    height: '20%', //
  },
  image: {
    width: 70,
    height: 70,
    borderRadius: 35,
    borderColor: '#111851',
    borderWidth: 1,
    margin: 7,
  },
  imageContainer: {
    alignItems: 'center',
  },
  userName: {
    fontSize: '20',
    color: '#111851',
    fontWeight: 'bold',
  },
  question: {
    fontSize: '15',
    color: '#111851',
  },
  content: {
    height: '80%',
    width: '100%'
  },
  contentText: {
    fontSize: '18',
    color: 'black',
    paddingTop: 13,
    paddingHorizontal: 8,
    textAlign: 'left'
  },
  bottom: {
    position: 'absolute',
    bottom: 0,
    flexDirection: 'row',
    justifyContent: 'space-around',
    width: '100%',
    alignItems: 'stretch',
    height: '39.5%', // Adjust as needed
    //backgroundColor: '#f0f0f0', // Add a background color to distinguish it
    //backgroundColor: '#E8EFF7',
    backgroundColor: 'F0F6FE',
    paddingVertical: 10,
    paddingHorizontal: 15,
  },
  iconItem: {
    flexDirection: 'column', // Arrange icons horizontally
    justifyContent: 'space-around', // Distribute icons evenly
    alignItems: 'left',
    width: '20%', // Take full width of the bottom view
    height: '100%',
    //backgroundColor: 'red',
    paddingHorizontal: 20
  },
  inputItem: {
    flexDirection: 'column', // Arrange icons horizontally
    justifyContent: 'space-around', // Distribute icons evenly
    alignItems: 'left',
    width: '90%', // Take full width of the bottom view
    height: '100%',
    // backgroundColor: 'yellow',
    paddingHorizontal: 15,
  },
  input: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    width: '100%',
    borderColor: '#31a1e5',
    borderWidth: 1,
    borderRadius: 10,
    backgroundColor: 'white',
    paddingHorizontal: 15,
    height: '10%',
  },
  imgText: {
    fontSize: '18',
    color: '#111851',
    paddingTop: 10,
    paddingHorizontal: 8
  }

})